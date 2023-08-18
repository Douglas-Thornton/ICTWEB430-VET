using System.ComponentModel;
using static APP.Data.Models.Models;

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
    }
}
