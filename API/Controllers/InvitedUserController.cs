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
        [Route("api/invitedUserController")]
        public class InvitedUserController : ControllerBase
        {

            private readonly VETDBContext _dbContext;

            public InvitedUserController(VETDBContext dbContext)
            {
                _dbContext = dbContext;
            }

            [HttpGet]
            public async Task<MainResponse> GetInvitedUsers()
            {
                var response = new MainResponse();
                try
                {
                    response.Content = _dbContext.InvitedUsers
                        .Include(ip => ip.User)
                        .Include(ip => ip.Meeting)
                        .Select(m => new InvitedUser
                        {
                            InviteID = m.InviteID,
                            UserID = m.UserID,
                            MeetingID = m.InviteID,
                            Accepted = m.Accepted,
                            ResponseDate = m.ResponseDate,
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
