using Microsoft.AspNetCore.Mvc;
using global::VETAPPAPI.Data;
using global::VETAPPAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VETAPPAPI.Controllers
{
    namespace VETAPPAPI.Controllers
    {
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
                        .Include(ip => ip.Meeting)
                        .Select(m => new InvitedPet
                        {
                            InvitedPetID = m.InvitedPetID,
                            PetID = m.PetID,
                            MeetingID = m.MeetingID,
                            InviteID = m.InviteID,
                            Pet = new Pet
                            {
                                PetID = m.Pet.PetID,
                                OwnerID = m.Pet.OwnerID,
                                PetName = m.Pet.PetName,
                                PetBreed = m.Pet.PetBreed,
                                PetAge = m.Pet.PetAge,
                                PetGender = m.Pet.PetGender,
                                PetPhotoFileLocation = m.Pet.PetPhotoFileLocation,
                                PetDiscoverability = m.Pet.PetDiscoverability                                
                            },
                            Meeting = new Meeting
                            {
                                MeetingID = m.Meeting.MeetingID,
                                UserCreated = m.Meeting.UserCreated,
                                MeetingDate = m.Meeting.MeetingDate,
                                MeetingLocation = m.Meeting.MeetingLocation,
                                MeetingCreationDate = m.Meeting.MeetingCreationDate,
                                MeetingCancellationDate = m.Meeting.MeetingCancellationDate,
                                MeetingName = m.Meeting.MeetingName,
                                MeetingMessage = m.Meeting.MeetingMessage 
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

        }
    }

}
