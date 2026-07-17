using Application.DTOs;
using Application.Interfaces.IUser;
using Domain.Common;
using Domain.Entities;
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