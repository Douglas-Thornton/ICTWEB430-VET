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
        [HttpPut("update/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User updatedUser)
        {
            var response = new MainResponse();
            try
            {
                // Check if a user with the given UserID exists in the database
                var existingUser = _dbContext.Users.Include(p => p.Pets).FirstOrDefault(u => u.UserID == userId);

                if (existingUser == null)
                {
                    // If the user does not exist, return a not found response
                    response.IsSuccess = false;
                    response.ErrorMessage = "User not found.";
                    return NotFound(response); // Return a 404 Not Found status
                }

                // Check if the updated username conflicts with other users
                if (_dbContext.Users.Any(u => u.LoginUsername == updatedUser.LoginUsername && u.UserID != userId))
                {
                    // Another user with the same username already exists, return an error response
                    response.IsSuccess = false;
                    response.ErrorMessage = "Username is not unique. Please choose a different username.";
                    return BadRequest(response); // Return a 400 Bad Request status
                }

                // Update the existing user's information with the data from the updatedUser object
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.Surname = updatedUser.Surname;
                existingUser.PhoneNumber = updatedUser.PhoneNumber;
                existingUser.Email = updatedUser.Email;
                existingUser.Suburb = updatedUser.Suburb;
                existingUser.Postcode = updatedUser.Postcode;
                existingUser.LoginUsername = updatedUser.LoginUsername;
                existingUser.LoginPassword = updatedUser.LoginPassword;
                existingUser.WebpageAnimalPreference = updatedUser.WebpageAnimalPreference;

                // You can also handle updating the user's pets if needed
                foreach (var updatedPet in updatedUser.Pets)
                {
                    var existingPet = existingUser.Pets.FirstOrDefault(p => p.PetID == updatedPet.PetID);

                    if (existingPet == null)
                    {
                        // If the pet doesn't exist in the user's current list, add it
                        existingUser.Pets.Add(updatedPet);
                    }
                    else
                    {
                        // If the pet exists, update its information
                        existingPet.PetName = updatedPet.PetName;
                        existingPet.PetBreed = updatedPet.PetBreed;
                        existingPet.PetAge = updatedPet.PetAge;
                        existingPet.PetGender = updatedPet.PetGender;
                        existingPet.PetPhoto = updatedPet.PetPhoto;
                        // Update other pet properties as needed
                    }
                }

                // Remove pets that are absent from the updated user's pet list
                foreach (var existingPet in existingUser.Pets.ToList())
                {
                    if (!updatedUser.Pets.Any(p => p.PetID == existingPet.PetID))
                    {
                        existingUser.Pets.Remove(existingPet);
                    }
                }
                _dbContext.SaveChanges();

                response.IsSuccess = true;
                response.Content = existingUser; // Return the updated user object
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            var response = new MainResponse();
            try
            {
                // You can add validation and other necessary logic here
                if (_dbContext.Users.Any(u => u.LoginUsername == newUser.LoginUsername))
                {
                    // A user with the same username already exists, return an error response
                    response.IsSuccess = false;
                    response.ErrorMessage = "Username is not unique. Please choose a different username.";
                    return BadRequest(response); // Return a 400 Bad Request status
                }

                // Save the new user to your database or perform any other required operations
                //newUser.UserID = null;
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                response.IsSuccess = true;
                response.Content = _dbContext.Users.Where(u => u.LoginUsername == newUser.LoginUsername && u.LoginPassword == newUser.LoginPassword)
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
