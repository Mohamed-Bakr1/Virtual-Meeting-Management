using Domain.Common;

namespace Application.Interfaces
{
    public interface ILiveKitService
    {
        Task<Result> CreateRoomAsync(string roomName, object metadata);
    }
}