using APP.States;

namespace APP
{
    public partial class App : Application
    {
        public App(LoggedUserState loggedUserState)
        {
            InitializeComponent();

            MainPage = new MainPage(loggedUserState);
        }
    }
}