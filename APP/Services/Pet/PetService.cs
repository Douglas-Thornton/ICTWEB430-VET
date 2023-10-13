using APP.Data.Models;
using APP.Interfaces;
using Newtonsoft.Json;

namespace APP.Services;

public class PetService : IPetService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<Pet>> GetAllPets()
    {

        List<Pet> pets = new List<Pet>();
        try
        {

            using var client = new HttpClient();

            string url = $"{_baseUrl}api/petController";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get pets");
            }

            string response = await apiResponse.Content.ReadAsStringAsync();
            MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

            if (deserializeResponse.IsSuccess)
            {
                pets = JsonConvert.DeserializeObject<List<Pet>>(deserializeResponse.Content.ToString());
            }
            else
            {
                ;

            }

            return pets;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
