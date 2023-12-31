﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VETAPPAPI.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Suburb { get; set; }
        public string? State { get; set; }
        public string? Postcode { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public AppPreferences? AppPreferences { get; set; }


        public virtual ICollection<Pet>? Pets { get; set; }


        public virtual ICollection<InvitedUser>? InvitedUsers { get; set; }
    }

    [Table("Pet")]
    public class Pet
    {
        [Key]
        public int PetID { get; set; }
        public int OwnerID { get; set; }
        public string? PetName { get; set; }
        public string? PetBreed { get; set; }
        public int? PetAge { get; set; }
        public string? PetGender { get; set; }
        public string? PetPhotoFileLocation { get; set; }
        public byte[]? PetPhoto { get; set; }
        public bool PetDiscoverability { get; set; }

        [ForeignKey("OwnerID")]
        public virtual User? Owner { get; set; }


        public virtual ICollection<InvitedPet>? InvitedPets { get; set; }
    }
    public class PetWithPicture
    {
        public Pet Pet { get; set; }
        public IFormFile Picture { get; set; }
    }

    [Table("Meeting")]
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }
        public int UserCreated { get; set; }
        public string? MeetingName { get; set; }
        public string? MeetingMessage { get; set; }
        public string? MeetingLocation { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? MeetingCreationDate { get; set; }
        public DateTime? MeetingCancellationDate { get; set; }

        [ForeignKey("UserCreated")]
        public virtual User? User { get; set; }
        public virtual ICollection<InvitedUser>? InvitedUsers { get; set; }

    }

    [Table("InvitedUser")]
    public class InvitedUser
    {
        [Key]
        public int InviteID { get; set; }
        public int UserID { get; set; }
        public int MeetingID { get; set; }
        public bool? Accepted { get; set; }
        public DateTime? ResponseDate { get; set; }

        [ForeignKey("UserID")]
        public virtual User? User { get; set; }

        public virtual List<InvitedPet>? InvitedPets { get; set; }
        public virtual Meeting? Meeting { get; set; }
    }

    [Table("InvitedPet")]
    public class InvitedPet
    {
        [Key]
        public int InvitedPetID { get; set; }
        public int PetID { get; set; }
        public int InviteID { get; set; }
        [ForeignKey("PetID")]
        public virtual Pet? Pet { get; set; }
        [ForeignKey("InviteID")]
        public virtual InvitedUser? InvitedUser { get; set; }
    }

    [Table("AppPreferences")]
    public class AppPreferences
    {
        [Key]
        public int UserID { get; set; }
        public string WebpageAnimalPreference { get; set; }
        public string SelectedFontSize { get; set; }
        public string SelectedFont { get; set; }
    }

    public class LoginRequest
    {
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
    }


    public class CreatedMeetingResponse
    {
        public int MeetingID { get; set; }
        public string Message { get; set; }
    }
}