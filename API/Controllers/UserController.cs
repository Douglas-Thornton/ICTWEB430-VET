using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VETAPPAPI.Data;
using VETAPPAPI.Models;

namespace VETAPPAPI.Controllers
{
    [ApiController]
    [Route("api/userController")]
    public class UserController : ControllerBase
    {

        private readonly VETDBContext _dbContext;

        public UserController(VETDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns>The set of users.</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Users
                .Select(u => new User
                {
                    UserID = u.UserID,
                    FirstName = u.FirstName,
                    Surname = u.Surname,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Suburb = u.Suburb,
                    Postcode = u.Postcode,
                    LoginUsername = u.LoginUsername,
                    LoginPassword = u.LoginPassword,
                    WebpageAnimalPreference = u.WebpageAnimalPreference,
                    Pets = u.Pets.Select(p => new Pet
                    {
                        PetID = p.PetID,
                        PetName = p.PetName,
                        PetBreed = p.PetBreed,
                        PetAge = p.PetAge,
                        PetGender = p.PetGender,
                        PetPhoto = p.PetPhoto,
                        PetDiscoverability = p.PetDiscoverability,
                        OwnerID = p.OwnerID // Include the OwnerID property if needed
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

        /// <summary>
        /// Gets a specific user through their ID.
        /// </summary>
        /// <param name="id">The id of the user to be foud.</param>
        /// <returns>a user.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetUser(int id)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Users.Where(u => u.UserID == id)
                .Select(u => new User
                {
                    UserID = u.UserID,
                    FirstName = u.FirstName,
                    Surname = u.Surname,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Suburb = u.Suburb,
                    Postcode = u.Postcode,
                    LoginUsername = u.LoginUsername,
                    LoginPassword = u.LoginPassword,
                    WebpageAnimalPreference = u.WebpageAnimalPreference,
                    Pets = u.Pets.Select(p => new Pet
                    {
                        PetID = p.PetID,
                        PetName = p.PetName,
                        PetBreed = p.PetBreed,
                        PetAge = p.PetAge,
                        PetGender = p.PetGender,
                        PetPhoto = p.PetPhoto,
                        PetDiscoverability = p.PetDiscoverability,
                        OwnerID = p.OwnerID // Include the OwnerID property if needed
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

        /// <summary>
        /// Try to login using a login username and password
        /// </summary>
        /// <param name="user">A user that contains a username, password</param>
        /// <returns>a user.</returns>
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Users.Where(u => u.LoginUsername == loginRequest.LoginUsername && u.LoginPassword == loginRequest.LoginPassword)
                .Select(u => new User
                {
                    UserID = u.UserID,
                    FirstName = u.FirstName,
                    Surname = u.Surname,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Suburb = u.Suburb,
                    Postcode = u.Postcode,
                    LoginUsername = u.LoginUsername,
                    LoginPassword = u.LoginPassword,
                    WebpageAnimalPreference = u.WebpageAnimalPreference,
                    Pets = u.Pets.Select(p => new Pet
                    {
                        PetID = p.PetID,
                        PetName = p.PetName,
                        PetBreed = p.PetBreed,
                        PetAge = p.PetAge,
                        PetGender = p.PetGender,
                        PetPhoto = p.PetPhoto,
                        PetDiscoverability = p.PetDiscoverability,
                        OwnerID = p.OwnerID // Include the OwnerID property if needed
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

        /// <summary>
        /// Deletes a user with all data created or 'owned' by them.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Success or fail of the delete.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Call the stored procedure using Entity Framework Core
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteUserAndRelatedData @p0", id);

                if (result > 0)
                {
                    return Ok($"User with ID {id} and related data deleted successfully.");
                }
                else
                {
                    return NotFound($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors
                return StatusCode(500, $"Error deleting user with ID {id}. {ex.Message}");
            }
        }

        /// <summary>
        /// Update an exisiting user with new data.
        /// </summary>
        /// <param name="updatedUser">The user to update.</param>
        /// <returns>Success or failure of the updated user.</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(User updatedUser)
        {
            try
            {
                // Validate the input data
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Get the existing user from the database
                var existingUser = await _dbContext.Users.FindAsync(updatedUser.UserID);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {updatedUser.UserID} not found.");
                }

                // Update the user's properties with the new values
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.Surname = updatedUser.Surname;
                existingUser.PhoneNumber = updatedUser.PhoneNumber;
                existingUser.Email = updatedUser.Email;
                existingUser.Suburb = updatedUser.Suburb;
                existingUser.Postcode = updatedUser.Postcode;
                existingUser.LoginUsername = updatedUser.LoginUsername;
                existingUser.LoginPassword = updatedUser.LoginPassword;
                existingUser.WebpageAnimalPreference = updatedUser.WebpageAnimalPreference;

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();

                // Return a response indicating success
                return Ok($"User with ID {existingUser.UserID} updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors
                return StatusCode(500, $"Error updating user. {ex.Message}");
            }
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            var response = new MainResponse();
            try
            {
                // You can add validation and other necessary logic here

                // Save the new user to your database or perform any other required operations
                //newUser.UserID = null;
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                response.IsSuccess = true;
                response.Content = "User created successfully"; // You can set any response message you want
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
