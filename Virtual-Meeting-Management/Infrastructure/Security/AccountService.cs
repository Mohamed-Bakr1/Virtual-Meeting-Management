using Application.DTOs;
using Application.Interfaces.IUser;
using Domain.Common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<InfrastructureUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<InfrastructureUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
        {
            var newUser = new InfrastructureUser
            {
                UserName = registerDto.Email.Split('.')[0],
                Email = registerDto.Email,
                FullName = registerDto.Name,
                Gender = registerDto.Gender
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                var result2 = await _userManager.AddToRoleAsync(newUser, "User");

                if (!result2.Succeeded)
                    return Result<string>.Failure(result2.Errors.Select(e => e.Description).ToList());

                await CreateClaimsAsync(newUser, "User");
                var token = _tokenService.GenerateToken(await _userManager.GetClaimsAsync(newUser));

                return Result<string>.Success(token, "User registered successfully.");
            }
            // THE FIX: Extract the actual errors so you know exactly why it failed
            var errors = result.Errors.Select(e => e.Description).ToList();

            // Optional: Log them properly
            // _logger.LogError("User creation failed: {Errors}", string.Join(", ", errors));

            return Result<string>.Failure(errors);
        }

        public async Task<Result> CheckEmailExistenceAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return Result.Failure("Email is already registered");
            }

            return Result.Success();
        }

        public async Task<Result<ValidLoginDto>> LoginAsync(LoginDto loginDto)
        {
            var checkUserExistence = await CheckEmailExistenceForLoginAsync(loginDto.Email);

            if (!checkUserExistence.IsSuccess)
                return Result<ValidLoginDto>.Failure("User not registered.");

            var checkPassword = await CheckPasswordMatchAsync(checkUserExistence.Data, loginDto.Password);

            if (!checkPassword.IsSuccess)
                return Result<ValidLoginDto>.Failure("Wrong Password!");

            var response = await PrepareLoginResponseAsync(checkUserExistence.Data);

            return Result<ValidLoginDto>.Success(response, "User login successfully!");
        }

        private async Task<Result<InfrastructureUser>> CheckEmailExistenceForLoginAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return Result<InfrastructureUser>.Success(user);
            }

            return Result<InfrastructureUser>.Failure("");
        }

        private async Task<Result> CheckPasswordMatchAsync(InfrastructureUser user, string password)
        {
            bool match = await _userManager.CheckPasswordAsync(user, password);

            if (!match)
                return Result.Failure("");

            return Result.Success();
        }

        private async Task<ValidLoginDto> PrepareLoginResponseAsync(InfrastructureUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var role = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(claims);

            var response = new ValidLoginDto
            {
                Token = token,
                Email = user.Email,
                Name = user.FullName,
                Roles = role
            };

            return response;
        }

        private async Task CreateClaimsAsync(InfrastructureUser User, string Role)
        {
            List<Claim> claims =
                [
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(ClaimTypes.Name, User.UserName),
                    new Claim(ClaimTypes.NameIdentifier, User.Id),
                    new Claim(ClaimTypes.Email, User.Email)
                ];

            await _userManager.AddClaimsAsync(User, claims);
        }
    }
}