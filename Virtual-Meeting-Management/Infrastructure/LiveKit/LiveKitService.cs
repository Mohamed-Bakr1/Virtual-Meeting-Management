using Application.Interfaces;
using Domain.Common;
using Livekit.Server.Sdk.Dotnet;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.LiveKit
{
    public class LiveKitService : ILiveKitService
    {
        private readonly RoomServiceClient _roomServiceClient;
        private readonly LiveKitSettings _settings;

        public LiveKitService(IOptions<LiveKitSettings> settings)
        {
            _settings = settings.Value;

            _roomServiceClient = new RoomServiceClient(
                apiKey: _settings.ApiKey,
                apiSecret: _settings.ApiSecret,
                host: _settings.Url
            );
        }

        public async Task<Result> CreateRoomAsync(string roomName, object metadata) //CancellationToken ct = default
        {
            //ct.ThrowIfCancellationRequested();

            var metadataJson = JsonSerializer.Serialize(metadata);

            var room = await _roomServiceClient.CreateRoom(new CreateRoomRequest
            {
                Name = roomName,
                Metadata = metadataJson,
                MaxParticipants = 40,
                EmptyTimeout = 2600,
                DepartureTimeout = 1800
            });

            if (room == null)
            {
                return Result.Failure(
                    "Room creation returned null!",
                    ErrorType.ServerError
                );
            }

            return Result.Success("Room Created Successfully!");
        }
    }
}