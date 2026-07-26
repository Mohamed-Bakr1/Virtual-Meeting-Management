using Application.DTOs.StreamingDto;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Virtual_Meeting_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly IUseCase<CreateMeetingDto, Result> _createMeetingUseCase;

        public MeetingController(IUseCase<CreateMeetingDto, Result> createMeetingUseCase)
        {
            _createMeetingUseCase = createMeetingUseCase;
        }

        [HttpPost("CreateMeeting")]
        public async Task<IActionResult> CreateMeetingAsync([FromBody] CreateMeetingDto createMeetingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _createMeetingUseCase.ExecuteAsync(createMeetingDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}