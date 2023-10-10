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
                response.Content = _dbContext.Meetings.Include(m => m.User).Include(m => m.InvitedUsers).ThenInclude(iu => iu.InvitedPets)
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
                     },
                     InvitedUsers = m.InvitedUsers.Select(m => new InvitedUser
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
                         InvitedPets = m.InvitedPets.Select(m => new InvitedPet
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
                             }
                         }).ToList(),
                     }).ToList()
                 }).ToList();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetMeetingById(int id)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Meetings.Include(m => m.InvitedUsers).ThenInclude(ip => ip.InvitedPets).Include(m => m.User)
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
                     },
                     InvitedUsers = m.InvitedUsers.Select(m => new InvitedUser
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
                         InvitedPets = m.InvitedPets.Select(m => new InvitedPet
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
                             }
                         }).ToList(),
                     }).ToList()
                 }).Where(m => m.MeetingID == id).ToList();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpGet("createdByUser/{id:int}")]
        public IActionResult GetMeetingsByCreatedUserID(int id)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Meetings.Include(m => m.InvitedUsers)
                 .ThenInclude(m => m.InvitedPets).Include(m => m.User)
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
                     },
                     InvitedUsers = m.InvitedUsers.Select(m => new InvitedUser
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
                         InvitedPets = m.InvitedPets.Select(m => new InvitedPet
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
                             }
                         }).ToList(),
                     }).ToList()
                 }).Where(m => m.UserCreated == id).ToList();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpGet("HasInvitedUser/{userId:int}")]
        public IActionResult GetMeetingsByInvitedUserId(int userId)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Meetings
                 .Include(m => m.InvitedUsers).ThenInclude(m => m.InvitedPets).Include(m => m.User)
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
                     },
                     InvitedUsers = m.InvitedUsers.Select(m => new InvitedUser
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
                             LoginPassword = m.User.LoginPassword
                         }
                     }).ToList()
                 }).Where(m => m.InvitedUsers.Any(u => u.UserID == userId)).ToList();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMeeting(Meeting meeting)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _dbContext.Meetings.Add(meeting);
                await _dbContext.SaveChangesAsync();

                return Ok($"Meeting created successfully with ID: {meeting.MeetingID}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating meeting. {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<MainResponse> UpdateMeeting(Meeting updatedMeeting)
        {
            var response = new MainResponse();

            try
            {

                var existingMeeting = await _dbContext.Meetings.FindAsync(updatedMeeting.MeetingID);
                if (existingMeeting == null)
                {
                    throw new Exception($"Meeting with ID {updatedMeeting.MeetingID} not found.");
                }

                // Update the meeting's properties with the new values
                existingMeeting.MeetingName = updatedMeeting.MeetingName;
                existingMeeting.MeetingMessage = updatedMeeting.MeetingMessage;
                existingMeeting.MeetingDate = updatedMeeting.MeetingDate;
                existingMeeting.MeetingCancellationDate = updatedMeeting.MeetingCancellationDate;
                existingMeeting.InvitedUsers = updatedMeeting.InvitedUsers;
                // Update other properties as needed

                await _dbContext.SaveChangesAsync();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            try
            {
                var meeting = await _dbContext.Meetings.FindAsync(id);
                if (meeting == null)
                {
                    return NotFound($"Meeting with ID {id} not found.");
                }

                _dbContext.Meetings.Remove(meeting);
                await _dbContext.SaveChangesAsync();

                return Ok($"Meeting with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting meeting with ID {id}. {ex.Message}");
            }
        }

    }
}
