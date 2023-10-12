using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Data.Models;


public class User : INotifyPropertyChanged
{
    public int UserID { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    [StringLength(16, ErrorMessage = "Phone number too long.(20 character limit).")]
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Suburb { get; set; }
    [StringLength(10, ErrorMessage = "Post code too long.(10 character limit).")]
    public string? Postcode { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    public string LoginUsername { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string LoginPassword { get; set; }


    public virtual ICollection<Pet> Pets { get; set; }

    private AppPreferences? _appPreferences = new AppPreferences();

    public event PropertyChangedEventHandler PropertyChanged;

    public AppPreferences? AppPreferences
    {
        get => _appPreferences;
        set
        {
            if (_appPreferences != value)
            {
                _appPreferences = value;
                OnPropertyChanged(nameof(AppPreferences));
            }
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Meeting
{
    public int MeetingID { get; set; }
    public int? UserCreated { get; set; }
    public DateTime? MeetingDate { get; set; }
    public string? MeetingLocation { get; set; }
    public DateTime? MeetingCreationDate { get; set; }
    public DateTime? MeetingCancelledDate { get; set; }
    public string? MeetingName { get; set; }
    public string? MeetingMessage { get; set; }
}


public class InvitedUser
{
    public int InviteID { get; set; }
    public int UserID { get; set; }
    public int MeetingID { get; set; }
    public bool? Accepted { get; set; }
    public DateTime? ResponseDate { get; set; }
}

public class InvitedPet
{
    public int InvitedPetID { get; set; }
    public int PetID { get; set; }
    public int MeetingID { get; set; }
    public int InviteID { get; set; }
}
public class Pet
{
    public int PetID { get; set; }
    public int OwnerID { get; set; }

    [Required(ErrorMessage = "Pet name required.")]
    [StringLength(16, ErrorMessage = "Pet name too long.(16 character limit).")]
    public string? PetName { get; set; }

    [Required(ErrorMessage = "Pet breed required.")]
    [StringLength(16, ErrorMessage = "Pet breed too long.(16 character limit).")]
    public string? PetBreed { get; set; }

    [Required(ErrorMessage = "Pet age required.")]

    public int? PetAge { get; set; }

    [Required(ErrorMessage = "Pet sex required.")]

    public string? PetGender { get; set; } = "Unknown";
    public string? PetPhotoFileLocation { get; set; }
    public byte[]? PetPhoto { get; set; }

    [Required(ErrorMessage = "Pet discoverability required.")]
    public bool PetDiscoverability { get; set; }
    public IBrowserFile PetPhotoUpload { get; set; }
    public virtual User Owner { get; set; }
}
public class AppPreferences : INotifyPropertyChanged
{
    private int _userID;
    public int UserID
    {
        get => _userID;
        set
        {
            if (_userID != value)
            {
                _userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }
    }

    private string _webpageAnimalPreference = UserAnimalPreference.Dog.ToString();
    public string WebpageAnimalPreference
    {
        get => _webpageAnimalPreference;
        set
        {
            if (_webpageAnimalPreference != value)
            {
                _webpageAnimalPreference = value;
                OnPropertyChanged(nameof(WebpageAnimalPreference));
            }
        }
    }

    private string _selectedFontSize = AccessibleFontSizes.Medium.ToString();
    public string SelectedFontSize
    {
        get => _selectedFontSize;
        set
        {
            if (_selectedFontSize != value)
            {
                _selectedFontSize = value;
                OnPropertyChanged(nameof(SelectedFontSize));
            }
        }
    }

    private string _selectedFont = AccessibleFonts.Arial.ToString();
    public string SelectedFont
    {
        get => _selectedFont;
        set
        {
            if (_selectedFont != value)
            {
                _selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class LoginRequest
{
    public string LoginUsername { get; set; }
    public string LoginPassword { get; set; }
}

public class ServiceResponse
{
    public string? Response { get; set; }
    public bool success { get; set; }
    public User? User { get; set; }

}

public enum ModalMode 
{ 
    Edit,
    Create
}

public enum UserAnimalPreference
{
    Dog,
    Cat,
    Mixed
}

public enum AccessibleFonts
{
    [Description("Arial, sans-serif")]
    Arial,

    [Description("Times New Roman, serif")]
    TimesNewRoman,

    [Description("Verdana, sans-serif")]
    Verdana,

    [Description("Tahoma, sans-serif")]
    Tahoma,

    [Description("Calibri, sans-serif")]
    Calibri,

    [Description("Helvetica, sans-serif")]
    Helvetica
}

public enum AccessibleFontSizes
{
    Small = 12,
    Medium = 16,
    Large = 20,
    XLarge = 24,
    XXLarge = 32
}

public enum PetImages 
{
    [Description("./Images/Background_Images/Dog_1.jpg")]
    dogImage1,
    [Description("./Images/Background_Images/Dog_2.jpg")]
    dogImage2,
    [Description("./Images/Background_Images/Dog_3.jpg")] // wont load
    dogImage3,
    [Description("./Images/Background_Images/Mixed_1.jpg")]
    mixedImage1,
    [Description("./Images/Background_Images/Mixed_2.jpg")]
    mixedImage2,
    [Description("./Images/Background_Images/Mixed_3.jpg")]
    mixedImage3,
    [Description("./Images/Background_Images/Cat_1.jpg")] // wont load
    catImage1,
    [Description("./Images/Background_Images/Cat_2.jpg")]
    catImage2,
    [Description("./Images/Background_Images/Cat_3.jpg")]
    catImage3
}

public enum DogImages
{
    [Description("./Images/Background_Images/Dog_1.jpg")]
    dogImage1,
    [Description("./Images/Background_Images/Dog_2.jpg")]
    dogImage2,
    [Description("./Images/Background_Images/Dog_3.jpg")]
    dogImage3
}

public enum CatImages
{
    [Description("./Images/Background_Images/Cat_1.jpg")]
    catImage1,
    [Description("./Images/Background_Images/Cat_2.jpg")]
    catImage2,
    [Description("./Images/Background_Images/Cat_3.jpg")]
    catImage3
}

public enum MixedImages
{
    [Description("./Images/Background_Images/Mixed_1.jpg")]
    mixedImage1,
    [Description("./Images/Background_Images/Mixed_2.jpg")]
    mixedImage2,
    [Description("./Images/Background_Images/Mixed_3.jpg")]
    mixedImage3
}

public class CreatedMeetingResponse
{
    public int MeetingID { get; set; }
    public string Message { get; set; }
}