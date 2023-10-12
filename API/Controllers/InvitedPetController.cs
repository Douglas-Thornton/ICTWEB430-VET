using Microsoft.AspNetCore.Mvc;
using VETAPPAPI.Data;
using VETAPPAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace VETAPPAPI.Controllers;

[ApiController]
[Route("api/invitedPetsController")]
public class InvitedPetController : ControllerBase
{

    private readonly VETDBContext _dbContext;

    public InvitedPetController(VETDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<MainResponse> GetInvitedPets()
    {
        var response = new MainResponse();
        try
        {
            response.Content = _dbContext.InvitedPets
                .Include(ip => ip.Pet)
                .Include(ip => ip.InvitedUser)
                .Select(m => new InvitedPet
                {
                    InvitedPetID = m.InvitedPetID,
                    PetID = m.PetID,
                    InviteID = m.InviteID,
                    Pet = new Pet
                    {
                        PetID = m.Pet.PetID,
                        OwnerID = m.Pet.OwnerID,
                        PetName = m.Pet.PetName,
                        PetBreed = m.Pet.PetBreed,
                        PetAge = m.Pet.PetAge,
                        PetGender = m.Pet.PetGender,
                        PetPhoto = m.Pet.PetPhoto,
                        PetDiscoverability = m.Pet.PetDiscoverability                                
                    },
                    InvitedUser = new InvitedUser 
                    { 
                        InviteID = m.InvitedUser.InviteID,
                        UserID = m.InvitedUser.UserID,
                        MeetingID = m.InvitedUser.InviteID,
                        Accepted = m.InvitedUser.Accepted,
                        ResponseDate = m.InvitedUser.ResponseDate
                    }
                }).ToList();

            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    [HttpPost]
    public async Task<MainResponse> CreateInvitedPet(InvitedPet invitedPet)
    {
        var response = new MainResponse();
        try
        {
            if (!ModelState.IsValid)
            {
                response.ErrorMessage = "Invalid input data.";
                response.IsSuccess = false;
                return response;
            }

            _dbContext.InvitedPets.Add(invitedPet);
            await _dbContext.SaveChangesAsync();

            response.Content = invitedPet;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    [HttpPut("{id:int}")]
    public async Task<MainResponse> UpdateInvitedPet(int id, InvitedPet invitedPet)
    {
        var response = new MainResponse();
        try
        {
            if (!ModelState.IsValid)
            {
                response.ErrorMessage = "Invalid input data.";
                response.IsSuccess = false;
                return response;
            }

            var existingInvitedPet = await _dbContext.InvitedPets.FindAsync(id);
            if (existingInvitedPet == null)
            {
                response.ErrorMessage = $"Invited pet with ID {id} not found.";
                response.IsSuccess = false;
                return response;
            }

            // Update properties of existingInvitedPet based on invitedPet

            await _dbContext.SaveChangesAsync();

            response.Content = existingInvitedPet;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    [HttpDelete("{id:int}")]
    public async Task<MainResponse> DeleteInvitedPet(int id)
    {
        var response = new MainResponse();
        try
        {
            var invitedPet = await _dbContext.InvitedPets.FindAsync(id);
            if (invitedPet == null)
            {
                response.ErrorMessage = $"Invited pet with ID {id} not found.";
                response.IsSuccess = false;
                return response;
            }

            _dbContext.InvitedPets.Remove(invitedPet);
            await _dbContext.SaveChangesAsync();

            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    //[HttpGet("bymeeting/{meetingId:int}")]
    //public async Task<MainResponse> GetInvitedPetsByMeetingId(int meetingId)
    //{
    //    var response = new MainResponse();
    //    try
    //    {
    //        response.Content = _dbContext.InvitedPets
    //            .Include(ip => ip.Pet)
    //            .Include(ip => ip.InvitedUser)
    //            .Include(ip => ip.Meeting)
    //            .Where(ip => ip.MeetingID == meetingId)
    //            .ToList();

    //        response.IsSuccess = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        response.ErrorMessage = ex.Message;
    //        response.IsSuccess = false;
    //    }

    //    return response;
    //}

}

