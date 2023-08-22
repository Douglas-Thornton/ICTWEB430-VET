using APP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Shared.Helpers
{
    public class UserPreferences
    {
        public UserPreferences(){}


        // Properties for user preferences
        public AccessibleFonts FontFamily { get; set; } = AccessibleFonts.Arial;
        public AccessibleFontSizes FontSize { get; set; } = AccessibleFontSizes.Medium;

        // Add more preferences properties as needed

        // Event to notify when preferences change
        public event EventHandler PreferencesChanged;

        // Method to update preferences and trigger the event
        public void UpdatePreferences(AccessibleFonts fontFamily, AccessibleFontSizes fontSize)
        {
            FontFamily = fontFamily;
            FontSize = fontSize;
            OnPreferencesChanged();
        }

        // Method to trigger the PreferencesChanged event
        protected virtual void OnPreferencesChanged()
        {
            PreferencesChanged?.Invoke(this, EventArgs.Empty);
        }

        public string PStyleTag 
        {
            get 
            { 
                return ActualFontFamily + " " + ActualFontSizeP;
            }
        }



        // Properties for actual font family and font size
        public string ActualFontFamily
        {
            get
            {
                switch (FontFamily)
                {
                    case AccessibleFonts.Arial:
                        return "font-family: Arial, sans-serif  !important;";
                    case AccessibleFonts.TimesNewRoman:
                        return "font-family: Times New Roman, serif !important;";
                    case AccessibleFonts.Verdana:
                        return "font-family: Verdana, sans-serif !important;";
                    case AccessibleFonts.Tahoma:
                        return "font-family: Tahoma, sans-serif !important;";
                    case AccessibleFonts.Calibri:
                        return "font-family: Calibri, sans-serif !important;";
                    case AccessibleFonts.Helvetica:
                        return "font-family: Helvetica, sans-serif !important;";
                    default:
                        return "font-family: Arial, sans-serif !important;";
                }
            }
        }

        public string ActualFontSizeP
        {
            get
            {
                switch (FontSize)
                {
                    case AccessibleFontSizes.Small:
                        return "font-size: 12px;";
                    case AccessibleFontSizes.Medium:
                        return "font-size: 16px;";
                    case AccessibleFontSizes.Large:
                        return "font-size: 20px;";
                    case AccessibleFontSizes.XLarge:
                        return "font-size: 24px;";
                    case AccessibleFontSizes.XXLarge:
                        return "font-size: 32px;";
                    default:
                        return "font-size: 16px;"; // Default to Medium
                }
            }
        }

        public string ActualFontSizeH1
        {
            get
            {
                switch (FontSize)
                {
                    case AccessibleFontSizes.Small:
                        return "font-size: 12px;";
                    case AccessibleFontSizes.Medium:
                        return "font-size: 16px;";
                    case AccessibleFontSizes.Large:
                        return "font-size: 20px;";
                    case AccessibleFontSizes.XLarge:
                        return "font-size: 24px;";
                    case AccessibleFontSizes.XXLarge:
                        return "font-size: 32px;";
                    default:
                        return "font-size: 16px;"; // Default to Medium
                }
            }
        }
    }
}
