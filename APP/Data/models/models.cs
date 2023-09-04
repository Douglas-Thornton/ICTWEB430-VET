using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace APP.Data.Models;


public class User
{
    public int UserID { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Suburb { get; set; }
    public string? Postcode { get; set; }
    public string LoginUsername { get; set; }
    public string LoginPassword { get; set; }
    public string WebpageAnimalPreference { get; set; }


    public virtual ICollection<Pet> Pets { get; set; }


}

public class LoginRequest
{
    public string LoginUsername { get; set; }
    public string LoginPassword { get; set; }
}

public class Pet
{
    public int PetID { get; set; }
    public int OwnerID { get; set; }
    public string? PetName { get; set; }
    public string? PetBreed { get; set; }
    public int? PetAge { get; set; }
    public string? PetGender { get; set; }
    public string? PetPhotoFileLocation { get; set; }
    public bool PetDiscoverability { get; set; }
    public IBrowserFile PetPhotoUpload { get; set; }
    public virtual User Owner { get; set; }


}
    public class Pet
    {
        public int PetID { get; set; }
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "Pet name required.")]
        [StringLength(16, ErrorMessage = "Pet name too long.(16 character limit).")]
        public string PetName { get; set; }

        [Required(ErrorMessage = "Pet breed required.")]
        [StringLength(16, ErrorMessage = "Pet breed too long.(16 character limit).")]
        public string PetBreed { get; set; }

        [Required(ErrorMessage = "Pet age required.")]
        public int PetAge { get; set; }

        [Required(ErrorMessage = "Pet sex required.")]
        public string PetGender { get; set; }

        public byte[] PetPhoto { get; set; }

        [Required(ErrorMessage = "Pet discoverability required.")]
        public bool PetDiscoverability { get; set; }
        public IBrowserFile PetPhotoUpload { get; set; }
        public virtual User Owner { get; set; }
    }
}
