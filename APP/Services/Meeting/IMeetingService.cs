using APP.Data.Models;

namespace APP.Services;

public interface IMeetingService
{
    public Task<List<Meeting>> GetMeetingsByMeetingId(int userId);

    public Task<CreatedMeetingResponse> CreateMeeting(Meeting meeting);
}
