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
        public UserPreferences() { }


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

        /// <summary>
        /// Returns combined family and size required to style a P tag. 
        /// </summary>
        public string PStyleTagCSS
        {
            get
            {
                return GetFontFamilyCSS + " " + GetFontSizePCSS;
            }
        }

        /// <summary>
        /// Returns combined family and size required to style a H1 tag. 
        /// </summary>
        public string H1StyleTag
        {
            get
            {
                return GetFontFamilyCSS + " " + GetFontSizeH1CSS;
            }
        }


        // Properties for font family for CSS.
        public string GetFontFamilyCSS
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

        // Properties for font size for a P tag for CSS.
        public string GetFontSizePCSS
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
                        return "font-size: 28px;";
                    default:
                        return "font-size: 16px;"; // Default to Medium
                }
            }
        }

        // Properties for font size for a H1 tag for CSS.
        public string GetFontSizeH1CSS
        {
            get
            {
                switch (FontSize)
                {
                    case AccessibleFontSizes.Small:
                        return "font-size: 24px;";
                    case AccessibleFontSizes.Medium:
                        return "font-size: 32px;";
                    case AccessibleFontSizes.Large:
                        return "font-size: 40px;";
                    case AccessibleFontSizes.XLarge:
                        return "font-size: 48px;";
                    case AccessibleFontSizes.XXLarge:
                        return "font-size: 56px;";
                    default:
                        return "font-size: 16px;"; // Default to Medium
                }
            }
        }

        // Properties for font family for XAML.
        public string GetFontFamilyXAML(AccessibleFonts font)
        {
            switch (font)
            {
                case AccessibleFonts.Arial:
                    return "Arial";
                case AccessibleFonts.TimesNewRoman:
                    return "Times New Roman";
                case AccessibleFonts.Verdana:
                    return "Verdana";
                case AccessibleFonts.Tahoma:
                    return "Tahoma";
                case AccessibleFonts.Calibri:
                    return "Calibri";
                case AccessibleFonts.Helvetica:
                    return "Helvetica";
                default:
                    return "Arial";
            }
        }

        // Properties for font size for XAML.
        public string GetFontSizeXAML(AccessibleFontSizes AccessibleFontSizes)
        {
            switch (AccessibleFontSizes)
            {
                case AccessibleFontSizes.Small:
                    return "12";
                case AccessibleFontSizes.Medium:
                    return "16";
                case AccessibleFontSizes.Large:
                    return "20";
                case AccessibleFontSizes.XLarge:
                    return "24";
                case AccessibleFontSizes.XXLarge:
                    return "28";
                default:
                    return "16"; // Default to Medium
            }
        }
    }
}
