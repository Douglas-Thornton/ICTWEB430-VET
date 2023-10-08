using APP.Data.Models;
using Newtonsoft.Json;

namespace APP.Services;

public class MeetingService : IMeetingService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<Meeting>> GetMeetingsByInvitedUserId(int userId)
    {

        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/HasInvitedUser/{userId}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode) 
            {
                throw new Exception("failed to get meetings");
            }

            var content = await apiResponse.Content.ReadAsStringAsync();
            var meetings = JsonConvert.DeserializeObject<List<Meeting>>(content);

            return meetings;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

