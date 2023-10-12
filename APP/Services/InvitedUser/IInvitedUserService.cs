using APP.Data.Models;

namespace APP.Services;

public interface IInvitedUserService
{
    public Task<List<InvitedUser>> GetInvitedUserById(int id);
    public Task<HttpResponseMessage> CreateInvitedUser(InvitedUser invitedUser);
    public Task<HttpResponseMessage> AcceptOrRejectInvite(AcceptMeetingResponse response);

}
