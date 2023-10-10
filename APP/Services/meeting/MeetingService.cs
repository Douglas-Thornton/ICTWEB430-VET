using APP.Data.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace APP.Services;

public class MeetingService : IMeetingService
{
    private readonly string _baseUrl = "https://localhost:7020/";

    public async Task<List<Meeting>> GetMeetingsByInvitedUserId(int userId)
    {
        List<Meeting> meetings = new List<Meeting>();
        try
        {

            using var client = new HttpClient();

            string url = $"{_baseUrl}api/meetingController/HasInvitedUser/{userId}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get meetings");
            }

            string response = await apiResponse.Content.ReadAsStringAsync();
            MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

            if (deserializeResponse.IsSuccess)
            {
                meetings = JsonConvert.DeserializeObject<List<Meeting>>(deserializeResponse.Content.ToString());
            }
            else
            {
                ;

            }

            return meetings;
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<ServiceResponse> CreateMeetingAsync(Meeting meetingToCreate)
    {
        ServiceResponse serviceResponse = new ServiceResponse { };

        try
        {
            var createdMeeting = new Meeting();
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/meetingController/create";
            var json = JsonConvert.SerializeObject(meetingToCreate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiResponse = await client.PostAsync(url, content);


            if (apiResponse.IsSuccessStatusCode)
            {
                // User creation was successful

                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    createdMeeting = JsonConvert.DeserializeObject<List<Meeting>>(deserializeResponse.Content.ToString()).FirstOrDefault();
                    serviceResponse.Meeting = createdMeeting;
                    serviceResponse.success = true;
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.Response = "Failed to login to new user";
                }
            }
            else
            {
                // Handle errors if necessary
                serviceResponse.Response = await apiResponse.Content.ReadAsStringAsync();
                serviceResponse.success = false;
            }

            return serviceResponse;
        }
        catch (Exception)
        {
            serviceResponse.success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateMeetingAsync(Meeting meetingToUpdate)
    {
        ServiceResponse serviceResponse = new ServiceResponse { };

        try
        {
            var updatedMeeting = new Meeting();
            using var client = new HttpClient();

            string url = $"{_baseUrl}api/meetingController/update";
            var json = JsonConvert.SerializeObject(meetingToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiResponse = await client.PutAsync(url, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    serviceResponse.success = true;
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.Response = "Failed to update meeting";
                }
            }
            else
            {
                // Handle errors if necessary
                serviceResponse.Response = await apiResponse.Content.ReadAsStringAsync();
                serviceResponse.success = false;
            }

            return serviceResponse;
        }
        catch (Exception)
        {
            serviceResponse.success = false;
            return serviceResponse;
        }
    }

    public async Task<List<Meeting>> GetMeetingsByCreatedUserId(int userId)
    {
        List<Meeting> meetings = new List<Meeting>();
        try
        {

            using var client = new HttpClient();

            string url = $"{_baseUrl}api/meetingController/createdByUser/{userId}";

            var apiResponse = await client.GetAsync(url);

            if (!apiResponse.IsSuccessStatusCode)
            {
                throw new Exception("failed to get meetings");
            }

            string response = await apiResponse.Content.ReadAsStringAsync();
            MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

            if (deserializeResponse.IsSuccess)
            {
                meetings = JsonConvert.DeserializeObject<List<Meeting>>(deserializeResponse.Content.ToString());
            }
            else
            {

            }

            return meetings;
        }
        catch (Exception)
        {
            throw;
        }
    }
}