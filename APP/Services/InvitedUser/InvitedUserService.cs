using APP.Data.Models;
using Newtonsoft.Json;

namespace APP.Services;

public class InvitedUserService: IInvitedUserService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<InvitedUser>> GetInvitedUserById(int id)
    {

        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/invitedUserController/byId/{id}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get the users");
            }

            var response = await apiResponse.Content.ReadAsStringAsync();
            var deserilizeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

            var users = JsonConvert.DeserializeObject<List<InvitedUser>>(deserilizeResponse.Content.ToString());
            
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
