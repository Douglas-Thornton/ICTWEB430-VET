using APP.Pages;
using APP.Shared.Helpers;
using System.ComponentModel;

namespace APP
{
    /// <summary>
    /// XAML main page, contains navigation links to other xaml pages and blazor webviews.
    /// </summary>
    public partial class MainPage : INotifyPropertyChanged
    {

        new public event PropertyChangedEventHandler PropertyChanged;
        private readonly UserPreferences _userPreferences;

        /// <summary>
        /// Constructor for the main page.
        /// </summary>
        /// <param name="userPreferences"></param>
        public MainPage(UserPreferences userPreferences)
        {

            InitializeComponent();

            this._userPreferences = userPreferences;
            _userPreferences.PreferencesChanged += OnUserPreferencesChange;
            UserPreferencesChanger userPreferencesChanger = new UserPreferencesChanger(userPreferences);
            Children.Add(userPreferencesChanger);

            BindingContext = this;
        }

        /// <summary>
        /// If properties change invoke this.
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// When user preferences change invoke this.
        /// Intended to update the font size and font family of xaml tabbed page headers.
        /// Does not seem to be possible as of now.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUserPreferencesChange(object sender, EventArgs e)
        {
            // Maui doesn't have controls for font-size and font for tabbed page?
        }
    }
}