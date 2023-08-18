using Newtonsoft.Json;
using static APP.Data.Models.Models;
using System.Net.Http.Json;

namespace APP.Data.Services;

public class UserService : IUserService
{
    private readonly string _baseUrl = "http://192.168.0.14:90/";

    public async Task<List<User>> GetAllUsersList()
    {
        try
        {
            var returnResponse = new List<User>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}api/userController";
                var apiResponse = await client.GetAsync(url);

                if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserilizeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                    if (deserilizeResponse.IsSuccess)
                    {
                        returnResponse = JsonConvert.DeserializeObject<List<User>>(deserilizeResponse.Content.ToString());
                    }
                }
                return returnResponse;
            }
        }
        catch (Exception ex)
        {
            return new List<User>();
        }
    }

    public async Task<User> LoginUser(LoginRequest loginRequest)
    {
        try
        {
            var returnResponse = new User();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}api/userController/login";
                var apiResponse = await client.PostAsJsonAsync(url, loginRequest);

                if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                    if (deserializeResponse.IsSuccess)
                    {
                        returnResponse = JsonConvert.DeserializeObject<User>(deserializeResponse.Content.ToString());


                    }
                }

                return null; // Handle other status codes or response scenarios
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}
