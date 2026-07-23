using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IUser;
using Domain.Common;

namespace Application.UseCases
{
    public class LoginUseCase : IUseCase<LoginDto, Result<ValidLoginDto>>
    {
        private readonly IAccountService _accountService;

        public LoginUseCase(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Result<ValidLoginDto>> ExecuteAsync(LoginDto loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            return result;
        }
    }
}