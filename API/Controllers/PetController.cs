using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Get all discoverable pets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MainResponse> GetPets()
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Pets
                .Where(p => p.PetDiscoverability)
                .Include(p => p.Owner)
                .Select(p => new Pet
                {
                    PetID = p.PetID,
                    PetName = p.PetName,
                    PetBreed = p.PetBreed,
                    PetAge = p.PetAge,
                    PetGender = p.PetGender,
                    PetPhoto = p.PetPhoto,
                    PetDiscoverability = p.PetDiscoverability,
                    OwnerID = p.OwnerID,
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
                    },
                    InvitedPets = p.InvitedPets.Select(p => new InvitedPet
                    {
                        InvitedPetID = p.InvitedPetID,
                        InviteID = p.InviteID,
                        PetID = p.PetID
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

            return response;
        }

        /// <summary>
        /// Gets a specific pet through its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to be found.</param>
        /// <returns>A pet.</returns>
        [HttpGet("pet/{id:int}")]
        public IActionResult GetPet(int id)
        {
            var response = new MainResponse();
            try
            {
                response.Content = _dbContext.Pets.Where(p => p.PetID == id)
                    .Select(p => new Pet
                    {
                        PetID = p.PetID,
                        PetName = p.PetName,
                        PetBreed = p.PetBreed,
                        PetAge = p.PetAge,
                        PetGender = p.PetGender,
                        PetPhoto = p.PetPhoto,
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
                        },
                        InvitedPets = p.InvitedPets.Select(p => new InvitedPet
                        {
                            InvitedPetID = p.InvitedPetID,
                            InviteID = p.InviteID,
                            PetID = p.PetID
                        }).ToList()
                    })
                    .FirstOrDefault(); // Retrieve a single pet or null if not found
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
        /// Delete a pet.
        /// </summary>
        /// <param name="id">The ID of the pet to delete.</param>
        /// <returns></returns>
        [HttpDelete("pet/{id:int}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            try
            {
                var pet = await _dbContext.Pets.FindAsync(id);
                if (pet == null)
                {
                    return NotFound($"Pet with ID {id} not found.");
                }

                _dbContext.Pets.Remove(pet);
                await _dbContext.SaveChangesAsync();

                return Ok($"Pet with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting pet with ID {id}. {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new pet.
        /// </summary>
        /// <param name="pet">The pet to create,</param>
        /// <returns></returns>
        [HttpPost("pet")]
        public async Task<IActionResult> CreatePet(Pet pet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _dbContext.Pets.Add(pet);
                await _dbContext.SaveChangesAsync();

                return Ok($"Pet created successfully with ID: {pet.PetID}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating pet. {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing pet.
        /// </summary>
        /// <param name="id">The id of the pet to update.</param>
        /// <param name="updatedPet">The updated pet.</param>
        /// <returns></returns>
        [HttpPut("pet/{id:int}")]
        public async Task<IActionResult> UpdatePet(int id, Pet updatedPet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingPet = await _dbContext.Pets.FindAsync(id);
                if (existingPet == null)
                {
                    return NotFound($"Pet with ID {id} not found.");
                }

                existingPet.PetName = updatedPet.PetName;
                existingPet.PetBreed = updatedPet.PetBreed;
                existingPet.PetAge = updatedPet.PetAge;
                existingPet.PetGender = updatedPet.PetGender;
                existingPet.PetPhoto = updatedPet.PetPhoto;
                existingPet.PetDiscoverability = updatedPet.PetDiscoverability;
                // Update other properties as needed

                await _dbContext.SaveChangesAsync();

                return Ok($"Pet with ID {id} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating pet. {ex.Message}");
            }
        }

    }
}
