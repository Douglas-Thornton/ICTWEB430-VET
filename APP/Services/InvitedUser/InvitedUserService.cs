using APP.Data.Models;
using Newtonsoft.Json;
using System.Text;

namespace APP.Services;

public class InvitedUserService: IInvitedUserService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<HttpResponseMessage> CreateInvitedUser(InvitedUser invitedUser)
    {
        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/invitedUserController/createInvitedUser";

            var body = JsonConvert.SerializeObject(invitedUser);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync(url, content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create invited user");
            }

            return apiResponse;
        }
        catch (Exception)
        {
            throw;
        }
    }

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

    public async Task<HttpResponseMessage> AcceptOrRejectInvite(InvitedUser invitedUser)
    {
        try
        {
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/invitedUserController/acceptInvite";
            var json = JsonConvert.SerializeObject(invitedUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var apiResponse = await client.PutAsync(url, content);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create invited user");
            }

            return apiResponse;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
