using APP.Shared.Enums;
using APP.Shared.Helpers;

namespace APP.Pages;

public partial class UserPreferencesChanger : ContentPage
{

	public AccessibleFonts userFont;
    public AccessibleFontSizes userFontSize;
    private UserPreferences userPreferences;

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

        fontPicker.SelectedItem = this.userPreferences.FontFamily.ToString();
        fontSizePicker.SelectedItem = this.userPreferences.FontSize.ToString();
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        if (Enum.TryParse(fontPicker.SelectedItem.ToString(), out AccessibleFonts selectedFont))
        {
            userFont = selectedFont;
        }

        if (Enum.TryParse(fontSizePicker.SelectedItem.ToString(), out AccessibleFontSizes selectedFontSize))
        {
            userFontSize = selectedFontSize;
        }

        // Update the UserPreferences singleton instance with the selected enum values
        userPreferences.UpdatePreferences(userFont, userFontSize);
    }
}