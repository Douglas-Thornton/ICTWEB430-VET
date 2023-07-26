using Newtonsoft.Json;
using APP.Data.models;
using APP.Data.services.Interfaces;
using APP.models;
using static APP.Data.models.models;

namespace VETAPP.Data.services
{
    public class UserService : IUserService
    {
        private string _baseUrl = "http://192.168.0.14:90/";
        async Task<List<models.User>> IUserService.GetAllUsersList()
        {
            try { 
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
    }
}
