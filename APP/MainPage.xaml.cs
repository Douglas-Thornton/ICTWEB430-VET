using APP.States;
using System.ComponentModel;
using APP.Data.Services;
using static APP.Data.Models.Models;

namespace APP
{
    public partial class MainPage : INotifyPropertyChanged
    {
        private readonly LoggedUserState _loggedUserState;

        private string _loginLogoutNavTitle = "Login";
        private string _loginLogoutNavType = "Login";
        private string _registerProfileNavTitle = "Register";
        private string _registerProfileNavType = "Register";
        public event PropertyChangedEventHandler PropertyChanged;

        public string LoginLogoutNavTitle
        {
            get { return _loginLogoutNavTitle; }
            set
            {
                if (_loginLogoutNavTitle != value)
                {
                    _loginLogoutNavTitle = value;
                    OnPropertyChanged(nameof(LoginLogoutNavTitle));
                }
            }
        }

        public string RegisterProfileNavTitle
        {
            get { return _registerProfileNavTitle; }
            set
            {
                if (_registerProfileNavTitle != value)
                {
                    _registerProfileNavTitle = value;
                    OnPropertyChanged(nameof(RegisterProfileNavTitle));
                }
            }
        }

        public string LoginLogoutNavType
        {
            get { return _loginLogoutNavType; }
            set
            {
                if (_loginLogoutNavType != value)
                {
                    _loginLogoutNavType = value;
                    OnPropertyChanged(nameof(LoginLogoutNavType));
                }
            }
        }

        public string RegisterProfileNavType
        {
            get { return _registerProfileNavType; }
            set
            {
                if (_registerProfileNavType != value)
                {
                    _registerProfileNavTitle = value;
                    OnPropertyChanged(nameof(RegisterProfileNavType));
                }
            }
        }


        public MainPage(LoggedUserState loggedUserState)
        {

            InitializeComponent();
            _loggedUserState = loggedUserState;
            BindingContext = this;

            _loggedUserState.PropertyChanged += OnLoggedUserStateChanged;

        }

        private void OnLoggedUserStateChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if the property that changed is "LoggedUser"
            if (e.PropertyName == nameof(_loggedUserState.LoggedUser))
            {
                UpdateNav();
            }
        }

        private void UpdateNav() 
        {
            if (_loggedUserState.LoggedUser != null) 
            {
                LoginLogoutNavTitle = "Logout";
                RegisterProfileNavTitle = "Profile";
            }
            else 
            {
                LoginLogoutNavTitle = "Login";
                RegisterProfileNavTitle = "Register";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}