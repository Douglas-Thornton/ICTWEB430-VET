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
                response.Content = _dbContext.Pets
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

        [HttpPost("petWithPicture")]
        public async Task<IActionResult> CreatePetWithPicture([FromForm] PetWithPicture petWithPicture)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Process uploaded picture from petWithPicture.Picture
                var pictureFileName = Guid.NewGuid().ToString() + Path.GetExtension(petWithPicture.Picture.FileName);
                var pictureFilePath = Path.Combine("Uploads", pictureFileName);
                using (var stream = new FileStream(pictureFilePath, FileMode.Create))
                {
                    await petWithPicture.Picture.CopyToAsync(stream);
                }

                // Create a new pet with picture information
                var newPet = new Pet
                {
                    // Assign pet properties from petWithPicture.Pet
                    PetName = petWithPicture.Pet.PetName,
                    PetBreed = petWithPicture.Pet.PetBreed,
                    // Assign other properties as needed
                    PetPhotoFileLocation = pictureFilePath
                };

                _dbContext.Pets.Add(newPet);
                await _dbContext.SaveChangesAsync();

                return Ok($"Pet created successfully with ID: {newPet.PetID}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating pet. {ex.Message}");
            }
        }

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
                existingPet.PetPhotoFileLocation = updatedPet.PetPhotoFileLocation;
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

        [HttpPut("petWithPicture/{id:int}")]
        public async Task<IActionResult> UpdatePetWithPicture(int id, [FromForm] PetWithPicture petWithPicture)
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

                // Process uploaded picture from petWithPicture.Picture
                if (petWithPicture.Picture != null)
                {
                    var pictureFileName = Guid.NewGuid().ToString() + Path.GetExtension(petWithPicture.Picture.FileName);
                    var pictureFilePath = Path.Combine("Uploads", pictureFileName);
                    using (var stream = new FileStream(pictureFilePath, FileMode.Create))
                    {
                        await petWithPicture.Picture.CopyToAsync(stream);
                    }

                    existingPet.PetPhotoFileLocation = pictureFilePath;
                }

                // Update pet properties
                existingPet.PetName = petWithPicture.Pet.PetName;
                existingPet.PetBreed = petWithPicture.Pet.PetBreed;
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
