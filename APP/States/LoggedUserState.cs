using System.ComponentModel;
using APP.Data.Models;

namespace APP.States
{
    public class LoggedUserState : INotifyPropertyChanged
    {
        private User _loggedUser { get; set; }

        public User LoggedUser
        {
            get => _loggedUser;
            set
            {
                _loggedUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedUser)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
        public string H1StyleTagCSS
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
                if (LoggedUser != null)
                {
                    if (Enum.TryParse(LoggedUser.AppPreferences.SelectedFont, out AccessibleFonts selectedFont))
                    {
                        switch (selectedFont)
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
                    else
                    {
                        // Handle the case where the string doesn't match any enum value
                        return "font-family: Arial, sans-serif !important;";
                    }
                }
                else
                {
                    // Handle the case where the string doesn't match any enum value
                    return "font-family: Arial, sans-serif !important;";
                }

            }
        }

        // Properties for font size for a P tag for CSS.
        public string GetFontSizePCSS
        {
            get
            {
                if (LoggedUser != null)
                {
                    if (Enum.TryParse(LoggedUser.AppPreferences.SelectedFontSize, out AccessibleFontSizes selectedFontSize))
                    {
                        switch (selectedFontSize)
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
                    else
                    {
                        return "font-size: 16px;"; // Default to Medium
                    }
                }
                else
                {
                    return "font-size: 16px;"; // Default to Medium
                }


            }
        }

        // Properties for font size for a H1 tag for CSS.
        public string GetFontSizeH1CSS
        {
            get
            {
                if (LoggedUser != null)
                {
                    if (Enum.TryParse(LoggedUser.AppPreferences.SelectedFontSize, out AccessibleFontSizes selectedFontSize))
                    {
                        switch (selectedFontSize)
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
                                return "font-size: 32px;"; // Default to Medium
                        }
                    }
                    else
                    {
                        return "font-size: 32px;"; // Default to Medium
                    }
                }
                else
                {
                    return "font-size: 32px;"; // Default to Medium
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
