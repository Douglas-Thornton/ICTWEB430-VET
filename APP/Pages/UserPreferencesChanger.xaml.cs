using APP.Shared.Enums;
using APP.Shared.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace APP.Pages;

public partial class UserPreferencesChanger : ContentPage, INotifyPropertyChanged
{
    public AccessibleFonts selectedUserFont = AccessibleFonts.Arial;
    public AccessibleFontSizes selectedUserFontSize = AccessibleFontSizes.Medium;
    private UserPreferences userPreferences;
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userPreferences">Inject user preferences</param>
    public UserPreferencesChanger(UserPreferences userPreferences)
    {
        this.userPreferences = userPreferences;
        InitializeComponent();

        foreach (AccessibleFonts accessibleFonts in Enum.GetValues(typeof(AccessibleFonts)))
        {
            fontPicker.Items.Add(accessibleFonts.ToString());
        }

        foreach (AccessibleFontSizes accessibleFontsSizes in Enum.GetValues(typeof(AccessibleFontSizes)))
        {
            fontSizePicker.Items.Add(accessibleFontsSizes.ToString());
        }

        selectedUserFont = this.userPreferences.FontFamily;
        selectedUserFontSize = this.userPreferences.FontSize;

        fontPicker.SelectedItem = this.userPreferences.FontFamily.ToString();
        fontSizePicker.SelectedItem = this.userPreferences.FontSize.ToString();

        BindingContext = this;
    }

    /// <summary>
    /// Getter and setter for the font family picker.
    /// When setting the value will return the selected value as a ENUM on selectedUserFont.
    /// </summary>
    public string selectedUserFontPicker 
    {
        get => selectedUserFont.ToString();
        set 
        {
            if (fontPicker.SelectedItem != null) 
            {
                if (Enum.TryParse(fontPicker.SelectedItem.ToString(), out AccessibleFonts selectedFont))
                {
                    selectedUserFont = selectedFont;
                    OnPropertyChanged(nameof(selectedUserFontPicker)); // Notify the UI that the property has changed
                }
            }
        }
    }

    /// <summary>
    /// Getter and setter for the font size picker.
    /// When setting the value will return the selected value as a ENUM on selectedUserFontSize.
    /// Will update UI for both the font size nmae ('Medium, Large, etc.) and font value (16, 18, etc.)
    /// </summary>
    public string selectedUserFontSizePicker
    {
        get => selectedUserFontSize.ToString();
        set
        {
            if(fontSizePicker.SelectedItem != null)
            {
                if (Enum.TryParse(fontSizePicker.SelectedItem.ToString(), out AccessibleFontSizes selectedFontSize))
                {
                    selectedUserFontSize = selectedFontSize;
                    OnPropertyChanged(nameof(selectedUserFontSizePicker)); // Notify the UI that the property has changed
                    OnPropertyChanged(nameof(selectedUserFontSizeValue)); // Notify the UI that the property has changed
                }
            }
        }
    }

    /// <summary>
    /// Getter for the font size value.
    /// returns
    /// </summary>
    public string selectedUserFontSizeValue
    {
        get => ((int)selectedUserFontSize).ToString(); // Convert enum to int and then to string
    }

    /// <summary>
    /// On save click update the values of the userpreferences class with the currently selected values.
    /// </summary>
    private void OnSaveClicked(object sender, EventArgs e)
    {
        // Update the UserPreferences singleton instance with the selected enum values
        userPreferences.UpdatePreferences(selectedUserFont, selectedUserFontSize);
    }

    /// <summary>
    /// Update the UI after changing values.
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}