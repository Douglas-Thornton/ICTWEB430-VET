using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VETAPPAPI.Data;
using VETAPPAPI.Models;

namespace VETAPPAPI.Controllers
{
    [ApiController]
    [Route("api/petController")]
    public class PetController : ControllerBase
    {

        private readonly VETDBContext _dbContext;

        public PetController(VETDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<MainResponse> GetPets()
        {
            var response = new MainResponse();
            try 
            {
                response.Content =_dbContext.Pets
                .Include(p => p.Owner)
                .Select(p => new Pet
                {
                    PetID = p.PetID,
                    PetName = p.PetName,
                    PetBreed = p.PetBreed,
                    PetAge = p.PetAge,
                    PetGender = p.PetGender,
                    PetPhotoFileLocation = p.PetPhotoFileLocation,
                    PetDiscoverability = p.PetDiscoverability,
                    Owner = new User
                    {
                        UserID = p.Owner.UserID,
                        FirstName = p.Owner.FirstName,
                        Surname = p.Owner.Surname,
                        PhoneNumber = p.Owner.PhoneNumber,
                        Email = p.Owner.Email,
                        Suburb = p.Owner.Suburb,
                        Postcode = p.Owner.Postcode,
                        LoginUsername = p.Owner.LoginUsername,
                        LoginPassword = p.Owner.LoginPassword,
                        WebpageAnimalPreference = p.Owner.WebpageAnimalPreference
                    },
                    InvitedPets = p.InvitedPets.Select(p => new InvitedPet
                    {
                        InvitedPetID = p.InvitedPetID,
                        InviteID = p.InviteID,
                        MeetingID = p.MeetingID,
                        PetID = p.PetID
                    }).ToList()
                })
                .ToList();

                response.IsSuccess = true;


            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }

    }
}
