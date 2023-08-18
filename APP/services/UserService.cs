using Newtonsoft.Json;
using static APP.Data.Models.Models;
using System.Net.Http.Json;
using APP.Data.Models;


namespace APP.Data.Services;

public class UserService : IUserService
{
    private readonly string _baseUrl = "http://192.168.0.14:90/";

    public async Task<List<User>> GetAllUsersList()
    {
        try
        {
            var returnResponse = new List<User>();
            using var client = new HttpClient();
            string url = $"{_baseUrl}api/userController";
            HttpResponseMessage apiResponse = await client.GetAsync(url);

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                var deserilizeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserilizeResponse.IsSuccess)
                {
                    returnResponse = JsonConvert.DeserializeObject<List<User>>(deserilizeResponse.Content.ToString());
                }
            }
            return returnResponse;
        }
        catch (Exception)
        {
            return new List<User>();
        }
    }

    public async Task<User> LoginUser(LoginRequest loginRequest)
    {
        try
        {
            var returnResponse = new User();
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/userController/login";
            HttpResponseMessage apiResponse = await client.PostAsJsonAsync(url, loginRequest);

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    returnResponse = JsonConvert.DeserializeObject<User>(deserializeResponse.Content.ToString());


                }
            }

            return null; // Handle other status codes or response scenarios
        }
        catch (Exception)
        {
            return null;
        }
    }

}
