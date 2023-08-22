using APP.Shared.Helpers;

namespace APP
{
    public partial class App : Application
    {
        public App(UserPreferences userPreferences)
        {
            InitializeComponent();

            MainPage = new MainPage(userPreferences);
        }
    }
}