using APP.Pages;
using APP.Shared.Helpers;
using System.ComponentModel;

namespace APP
{
    public partial class MainPage : INotifyPropertyChanged
    {

        new public event PropertyChangedEventHandler PropertyChanged;
        private readonly UserPreferences _userPreferences;

        public MainPage(UserPreferences userPreferences)
        {

            InitializeComponent();

            this._userPreferences = userPreferences;
            _userPreferences.PreferencesChanged += OnUserPreferencesChange;
            UserPreferencesChanger userPreferencesChanger = new UserPreferencesChanger(userPreferences);
            Children.Add(userPreferencesChanger);

            BindingContext = this;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnUserPreferencesChange(object sender, EventArgs e)
        {
            // Maui doesn't have controls for font-size and font for tabbed page?
        }
    }
}