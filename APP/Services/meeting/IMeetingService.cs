using APP.Data.Models;

namespace APP.Services;

public interface IMeetingService
{
    public Task<List<Meeting>> GetMeetingsByInvitedUserId(int userId);
    public Task<List<Meeting>> GetMeetingsByCreatedUserId(int userId);

    public Task<ServiceResponse> CreateMeetingAsync(Meeting meetingToCreate);
    public Task<ServiceResponse> UpdateMeetingAsync(Meeting meetingToCreate);

}