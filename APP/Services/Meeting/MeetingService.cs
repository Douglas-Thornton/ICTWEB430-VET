using APP.Data.Models;
using Newtonsoft.Json;

namespace APP.Services;

public class MeetingService : IMeetingService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<Meeting>> GetMeetingsByMeetingId(int meetingId)
    {

        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/meetingController/{meetingId}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get the users");
            }

            var response = await apiResponse.Content.ReadAsStringAsync();
            var deserilizeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

            var meetings = JsonConvert.DeserializeObject<List<Meeting>>(deserilizeResponse.Content.ToString());

            return meetings;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

