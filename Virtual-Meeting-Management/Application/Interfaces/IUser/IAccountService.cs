using Application.DTOs.AuthenticationDto;
using Domain.Common;

namespace Application.Interfaces.IUser
{
    public interface IAccountService
    {
        Task<Result<ValidLoginDto>> LoginAsync(LoginDto loginDto);
        Task<Result<string>> RegisterAsync(RegisterDto registerDto);
        Task<Result> CheckEmailExistenceAsync(string email);
        //Task<Result<UserDto>> CheckEmailExistenceForLoginAsync(string email);
        //Task<Result> CheckPasswordMatchAsync(ApplicationUser user, string password);
        //Task<Result<ExternalLoginClaimsDTO>> ValidateAndExtractExternalLoginClaims(ExternalLoginDto externalLoginDto, ClaimsPrincipal externalLoginClaims);
        //Task<ValidloginDto> PrepareLoginResponseAsync(ApplicationUser user);
        //Task<Result<ValidloginDto>> StoreExternalLoginAsync(string providerKey, string provider, ApplicationUser user);
        //Task<Result> StoreUserBasedOnRoleAsync(string userID, string role, string EducationLevelOrSubject);
        //Task<Result<UserLoginDataDto>> VerifyOtpAsync(VerifyOTPDto verifyOTPDto);
        //Task<Result> ForgetPasswordAsync(ApplicationUser user, string newPassword);
    }
}