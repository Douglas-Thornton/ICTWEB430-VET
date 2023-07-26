using APP.Data.services.Interfaces;
using APP.States;
using VETAPP.Data.services;
using static APP.Data.models.models;

namespace APP
{
    public partial class MainPage
    {

        public MainPage(LoggedUserState loggedUserState)
        {

            InitializeComponent();
            _loggedUserState = loggedUserState;
            BindingContext = _loggedUserState;
        }

        private readonly LoggedUserState _loggedUserState;
    }
}