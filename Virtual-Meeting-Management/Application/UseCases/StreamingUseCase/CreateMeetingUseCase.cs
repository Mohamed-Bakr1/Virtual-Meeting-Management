using Application.DTOs.StreamingDto;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.StreamingUseCase
{
    public class CreateMeetingUseCase : IUseCase<CreateMeetingDto, Result>
    {
        private readonly ILiveKitService _liveKitService;
        private readonly IUserContextService _userContextService;
        private readonly IRepository<Meeting> _meetingRepo;

        public CreateMeetingUseCase(ILiveKitService liveKitService,
            IUserContextService userContextService,
            IRepository<Meeting> meetingRepo)
        {
            _liveKitService = liveKitService;
            _userContextService = userContextService;
            _meetingRepo = meetingRepo;
        }

        public async Task<Result> ExecuteAsync(CreateMeetingDto createClassDto)
        {
            var userId = _userContextService.GetUserID();

            var meeting = new Meeting
            {
                Title = createClassDto.Title,
                Description = createClassDto.Description,
                StartTime = createClassDto.StartTime,
                EndTime = createClassDto.EndTime,
                HostId = userId
            };

            var result = await _liveKitService.CreateRoomAsync(meeting.Id, new { meeting.Title, meeting.HostId });

            if (result.IsSuccess)
            {
                await _meetingRepo.AddAsync(meeting);
                await _meetingRepo.SaveChangesAsync();
            }

            return result;
        }
    }
}