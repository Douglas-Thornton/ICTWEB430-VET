using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VETAPPAPI.Data;
using VETAPPAPI.Models;

namespace VETAPPAPI.Controllers
{
    [ApiController]
    [Route("api/meetingController")]
    public class MeetingController : ControllerBase
    {

        private readonly VETDBContext _dbContext;

        public MeetingController(VETDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetMeetings()
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Meetings
                .Include(m => m.InvitedPets).Include(m => m.InvitedUsers).Include(m => m.User)
                .Select(m => new Meeting
                {
                    MeetingID = m.MeetingID,
                    UserCreated = m.UserCreated,
                    MeetingDate = m.MeetingDate,
                    MeetingLocation = m.MeetingLocation,
                    MeetingCreationDate = m.MeetingCreationDate,
                    MeetingCancellationDate = m.MeetingCancellationDate,
                    MeetingName = m.MeetingName,
                    MeetingMessage = m.MeetingMessage,
                    User = new User
                    {
                        UserID = m.User.UserID,
                        FirstName = m.User.FirstName,
                        Surname = m.User.Surname,
                        PhoneNumber = m.User.PhoneNumber,
                        Email = m.User.Email,
                        Suburb = m.User.Suburb,
                        Postcode = m.User.Postcode,
                        LoginUsername = m.User.LoginUsername,
                        LoginPassword = m.User.LoginPassword,
                        WebpageAnimalPreference = m.User.WebpageAnimalPreference
                    },
                    InvitedPets = m.InvitedPets.Select(m => new InvitedPet
                    {
                        InvitedPetID = m.InvitedPetID,
                        InviteID = m.InviteID,
                        MeetingID = m.MeetingID,
                        PetID = m.PetID
                    }).ToList(),
                    InvitedUsers = m.InvitedUsers.Select(m => new InvitedUser
                    {
                        InviteID = m.InviteID,
                        UserID = m.User.UserID,
                        MeetingID =m.MeetingID,
                        Accepted = m.Accepted,
                        ResponseDate = m.ResponseDate
                    }).ToList()
                }) 
                .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }



    }
}
