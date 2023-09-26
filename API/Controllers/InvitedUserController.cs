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

            [HttpGet("bymeeting/{meetingId:int}")]
            public async Task<MainResponse> GetInvitedUsersByMeetingId(int meetingId)
            {
                var response = new MainResponse();
                try
                {
                    response.Content = _dbContext.InvitedUsers
                        .Include(ip => ip.User)
                        .Include(ip => ip.Meeting)
                        .Where(ip => ip.MeetingID == meetingId)
                        .ToList();

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
            public async Task<MainResponse> CreateInvitedUser(InvitedUser invitedUser)
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

                    _dbContext.InvitedUsers.Add(invitedUser);
                    await _dbContext.SaveChangesAsync();

                    response.Content = invitedUser;
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
            public async Task<MainResponse> UpdateInvitedUser(int id, InvitedUser invitedUser)
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

                    var existingInvitedUser = await _dbContext.InvitedUsers.FindAsync(id);
                    if (existingInvitedUser == null)
                    {
                        response.ErrorMessage = $"Invited user with ID {id} not found.";
                        response.IsSuccess = false;
                        return response;
                    }

                    // Update properties of existingInvitedUser based on invitedUser

                    await _dbContext.SaveChangesAsync();

                    response.Content = existingInvitedUser;
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
            public async Task<MainResponse> DeleteInvitedUser(int id)
            {
                var response = new MainResponse();
                try
                {
                    var invitedUser = await _dbContext.InvitedUsers.FindAsync(id);
                    if (invitedUser == null)
                    {
                        response.ErrorMessage = $"Invited user with ID {id} not found.";
                        response.IsSuccess = false;
                        return response;
                    }

                    _dbContext.InvitedUsers.Remove(invitedUser);
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

        }
    }

}
