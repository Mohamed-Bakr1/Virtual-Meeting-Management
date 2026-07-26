using Application.DTOs.AuthenticationDto;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Virtual_Meeting_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUseCase<RegisterDto, Result> _registerUseCase;
        private readonly IUseCase<LoginDto, Result<ValidLoginDto>> _loginUseCase;

        public AccountController(IUseCase<RegisterDto, Result> registerUseCase,
            IUseCase<LoginDto, Result<ValidLoginDto>> loginUseCase)
        {
            _registerUseCase = registerUseCase;
            _loginUseCase = loginUseCase;
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
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
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

        #region documentation
        /// <summary>
        /// User login API.
        /// </summary>
        /// <remarks>
        ///  Login using email and password.
        /// If the user didn’t verify by OTP in the last 30 days, an OTP will be sent to his email.
        ///
        /// Example:
        /// {
        ///   "email": "user@example.com",
        ///   "password": "Abcd1234#"
        /// }
        ///
        /// Responses:
        /// - 200: Login successful (returns token)
        /// - 403: OTP required Check your Email (sends code to email)
        /// - 401: Wrong email or password
        /// </remarks>
        /// 
        #endregion
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginUseCase.ExecuteAsync(loginDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
