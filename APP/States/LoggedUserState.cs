using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APP.Data.models.models;

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
