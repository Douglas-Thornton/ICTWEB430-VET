using APP.Data.Models;
using Newtonsoft.Json;

namespace APP.Services;

public class InvitedUserService: IInvitedUserService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<InvitedUser>> GetInvitedUserByMeetingId(int meetingId)
    {

        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/bymeeting/{meetingId}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get the users");
            }

            var content = await apiResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<InvitedUser>>(content);

            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
