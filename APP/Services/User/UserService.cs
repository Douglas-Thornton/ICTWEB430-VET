﻿using APP.Data.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using APP.Interfaces;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using APP.Helper;

namespace APP.Services;

public class UserService : IUserService
{
    private readonly IConfiguration configuration;
    public PasswordHasher passwordHasher = new PasswordHasher();

    public UserService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private readonly string _baseUrl = "https://localhost:7020/";

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

            LoginRequest hashedLogin = new LoginRequest()
            {
                LoginUsername = loginRequest.LoginUsername,
                LoginPassword = passwordHasher.ComputeSha512Hash(loginRequest.LoginPassword)
            };

            string url = $"{_baseUrl}api/userController/login";
            HttpResponseMessage apiResponse = await client.PostAsJsonAsync(url, hashedLogin);

            if (apiResponse.IsSuccessStatusCode)
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    returnResponse = JsonConvert.DeserializeObject<List<User>>(deserializeResponse.Content.ToString()).FirstOrDefault();
                    returnResponse.LoginPassword = string.Empty;
                    return returnResponse;
                }
            }
            return null; // Handle other status codes or response scenarios
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<ServiceResponse> CreateUserAsync(User userToCreate)
    {
        ServiceResponse serviceResponse = new() { };

        try
        {
            var createdUser = new User();
            using var client = new HttpClient();

            User hashedUserToCreate = new User
            {
                UserID = userToCreate.UserID,
                FirstName = userToCreate.FirstName,
                Surname = userToCreate.Surname,
                PhoneNumber = userToCreate.PhoneNumber,
                Email = userToCreate.Email,
                AddressLine1 = userToCreate.AddressLine1,
                AddressLine2 = userToCreate.AddressLine2,
                Suburb = userToCreate.Suburb,
                State = userToCreate.State,
                Postcode = userToCreate.Postcode,
                LoginUsername = userToCreate.LoginUsername,
                LoginPassword = passwordHasher.ComputeSha512Hash(userToCreate.LoginPassword),
                ErrorMessages = userToCreate.ErrorMessages,
                AppPreferences = userToCreate.AppPreferences,
                Pets = userToCreate.Pets
            };


            string url = $"{_baseUrl}api/userController/create";
            var json = JsonConvert.SerializeObject(hashedUserToCreate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiResponse = await client.PostAsync(url, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                // User creation was successful

                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    createdUser = JsonConvert.DeserializeObject<List<User>>(deserializeResponse.Content.ToString()).FirstOrDefault();
                    createdUser.LoginPassword = string.Empty;
                    serviceResponse.User = createdUser;
                    serviceResponse.success = true;
                }
                else 
                { 
                    serviceResponse.success= false;
                    serviceResponse.Response = "Failed to create new user";
                }
            }
            else
            {
                // Handle errors if necessary
                serviceResponse.Response = await apiResponse.Content.ReadAsStringAsync();
                serviceResponse.success = true;
            }

            return serviceResponse;
        }
        catch (Exception)
        {
            serviceResponse.success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateUserAsync(User userToUpdate)
    {
        ServiceResponse serviceResponse = new() { };

        try
        {
            var updatedUser = new User();
            using var client = new HttpClient();

            User hashedUserToCreate = new User
            {
                UserID = userToUpdate.UserID,
                FirstName = userToUpdate.FirstName,
                Surname = userToUpdate.Surname,
                PhoneNumber = userToUpdate.PhoneNumber,
                Email = userToUpdate.Email,
                AddressLine1 = userToUpdate.AddressLine1,
                AddressLine2 = userToUpdate.AddressLine2,
                Suburb = userToUpdate.Suburb,
                State = userToUpdate.State,
                Postcode = userToUpdate.Postcode,
                LoginUsername = userToUpdate.LoginUsername,
                LoginPassword = passwordHasher.ComputeSha512Hash(userToUpdate.LoginPassword),
                ErrorMessages = userToUpdate.ErrorMessages,
                AppPreferences = userToUpdate.AppPreferences,
                Pets = userToUpdate.Pets
            };

            string url = $"{_baseUrl}api/userController/update";
            var json = JsonConvert.SerializeObject(hashedUserToCreate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiResponse = await client.PutAsync(url, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                // User creation was successful

                string response = await apiResponse.Content.ReadAsStringAsync();
                MainResponseModel deserializeResponse = JsonConvert.DeserializeObject<MainResponseModel>(response);

                if (deserializeResponse.IsSuccess)
                {
                    updatedUser = JsonConvert.DeserializeObject<List<User>>(deserializeResponse.Content.ToString()).FirstOrDefault();
                    updatedUser.LoginPassword = string.Empty;
                    serviceResponse.User = updatedUser;
                    serviceResponse.success = true;
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.Response = "Failed to update user";
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

    public async Task<ServiceResponse> ValidateAddress(User userToValidate)
    {

        ServiceResponse serviceResponse = new ServiceResponse { };

        String AddressAPI_Secret = configuration["AddressAPISecret"];
        String AddressAPI_Key = configuration["AddressAPIKey"];
        string AddressToValidate = "";

        if (!string.IsNullOrEmpty(userToValidate.AddressLine1))
        {
            AddressToValidate += userToValidate.AddressLine1 + " ";
        }

        if (!string.IsNullOrEmpty(userToValidate.AddressLine2))
        {
            AddressToValidate += userToValidate.AddressLine2 + " ";
        }

        if (!string.IsNullOrEmpty(userToValidate.Suburb))
        {
            AddressToValidate += userToValidate.Suburb + " ";
        }

        if (!string.IsNullOrEmpty(userToValidate.State))
        {
            AddressToValidate += userToValidate.State;
        }

        AddressToValidate = AddressToValidate.ToUpper();
        AddressToValidate = AddressToValidate.Replace(" ", "%");


        try
        {
            var updatedUser = new User();
            using var client = new HttpClient();

            string url = $"https://api.addressfinder.io/api/au/address/v2/verification/?key={AddressAPI_Key}&format=json&q={AddressToValidate}&gnaf=1&paf=1";
            client.DefaultRequestHeaders.Add("Authorization", AddressAPI_Secret);
            
            var apiResponse = await client.GetAsync(url);

            if (apiResponse.IsSuccessStatusCode)
            {

                string response = await apiResponse.Content.ReadAsStringAsync();
                AddressVerificationResponse deserializeResponse = JsonConvert.DeserializeObject<AddressVerificationResponse>(response);

                if (deserializeResponse.Success && deserializeResponse.Matched)
                {
                    serviceResponse.Response = response;
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
}
