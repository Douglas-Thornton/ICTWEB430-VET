
using APP.Data.Models;

namespace APP.Services;

public interface IInvitedUserService
{
    public Task<List<InvitedUser>> GetInvitedUserByMeetingId(int meetingId);
}
