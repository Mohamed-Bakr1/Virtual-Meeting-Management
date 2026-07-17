using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Virtual_Meeting_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUseCase<RegisterDto, Result> _registerUseCase;

        public AccountController(IUseCase<RegisterDto, Result> registerUseCase)
        {
            _registerUseCase = registerUseCase;
        }

        #region Documentation
        /// <summary>
        /// 📩 Register new user (Teacher or Student)
        /// </summary>
        /// <remarks>
        /// ### Description:
        /// This endpoint is used when a user creates a new account.
        /// - The user provides: FullName, Email, Password, Role ("teacher" or "student"), and Subject/EducationLevel.
        /// - After registration, an **email confirmation link** will be sent to the user’s email.
        /// - The user **cannot log in** until they verify their email
        /// 
        /// </remarks>
        /// <response code="200">User created successfully and confirmation email sent.</response>
        /// <response code="400">Invalid input or email already exists.</response> 
        #endregion
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerUseCase.ExecuteAsync(registerDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
