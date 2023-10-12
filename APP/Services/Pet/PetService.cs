using APP.Data.Models;
using APP.Interfaces;
using Newtonsoft.Json;

namespace APP.Services;

public class PetService : IPetService
{
    private readonly string _baseUrl = "https://localhost:7020/";
    public async Task<List<Pet>> GetAllPets()
    {
        try
        {
            var returnResponse = new List<Pet>();
            using var client = new HttpClient();
            string url = $"{_baseUrl}api/petController";
            HttpResponseMessage apiResponse = await client.GetAsync(url);

            if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                var deserilizeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserilizeResponse.IsSuccess)
                {
                    returnResponse = JsonConvert.DeserializeObject<List<Pet>>(deserilizeResponse.Content.ToString());
                }
            }
            return returnResponse;
        }
        catch (Exception)
        {
            return new List<Pet>();
        }
    }
}
