﻿using Microsoft.AspNetCore.Mvc;
using VETAPPAPI.Data;
using VETAPPAPI.Models;
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

            [HttpGet("byId/{id:int}")]
            public async Task<MainResponse> GetInvitedUsersById(int id)
            {
                var response = new MainResponse();
                try
                {
                    response.Content = _dbContext.InvitedUsers
                        .Include(ip => ip.User)
                        .Include(ip => ip.Meeting)
                        .Include(ip => ip.InvitedPets)
                        .ThenInclude(p => p.Pet)
                        .ThenInclude(o => o.Owner)
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
                            },
                            InvitedPets = m.InvitedPets.Select(p => new InvitedPet
                            {
                                PetID = p.PetID,
                                InvitedPetID = p.InvitedPetID,
                                InviteID = p.InviteID,
                                Pet = new Pet 
                                {
                                    PetID = p.Pet.PetID,
                                    PetName = p.Pet.PetName,
                                    PetBreed = p.Pet.PetBreed,
                                    PetAge = p.Pet.PetAge,
                                    PetGender = p.Pet.PetGender,
                                    PetPhoto = p.Pet.PetPhoto,
                                    PetDiscoverability = p.Pet.PetDiscoverability,
                                    OwnerID = p.Pet.OwnerID
                                }
                            }).ToList()
                        }).Where(user => user.UserID == id).ToList();

                    response.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.IsSuccess = false;
                }

                return response;
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


            [HttpPost("createInvitedUser")]
            public async Task<MainResponse> CreateInvitedUser([FromBody] InvitedUser invitedUser)
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
            public async Task<MainResponse> UpdateInvitedUser(int id)
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

            [HttpPut("acceptInvite")]
            public async Task<MainResponse> AcceptInvite(InvitedUser invitedUser)
            {
                var response = new MainResponse();
                try
                {
                    var existingInvite = await _dbContext.InvitedUsers.FindAsync(invitedUser.MeetingID);
                    if (existingInvite == null)
                    {
                        throw new Exception($"Meeting with ID {invitedUser.MeetingID} not found.");
                    }

                    // Update the invites's properties with the new values
                    existingInvite.ResponseDate = invitedUser.ResponseDate;
                    existingInvite.Accepted = invitedUser.Accepted;

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
