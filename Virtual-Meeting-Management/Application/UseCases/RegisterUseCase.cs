using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IUser;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class RegisterUseCase : IUseCase<RegisterDto, Result>
    {
        private readonly IAccountService _accountService;

        public RegisterUseCase(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Result> ExecuteAsync(RegisterDto registerDto)
        {
            var CheckEmailExistence = await _accountService.CheckEmailExistenceAsync(registerDto.Email);

            if (!CheckEmailExistence.IsSuccess)
                return CheckEmailExistence;

            var result = await _accountService.RegisterAsync(registerDto);

            return result;
        }
    }
}