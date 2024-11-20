using BUMA.Data;
using BUMA.Models;
using BUMA.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YourApp.Views;

namespace BUMA.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private Window? _loginWindow;
        private readonly UserService _userService;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand NewUserCommand { get; private set; } 

        private void OpenRegisterView()
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();

            _loginWindow?.Close();
        }

        public void SetWindowInstance(Window window)
        {
            _loginWindow = window;
        }
   
        public LoginViewModel(UserService userService)
        {
            LoginCommand = new RelayCommand(Login);
            NewUserCommand = new RelayCommand(OpenRegisterView);
            _userService = userService;
        }

        private void Login()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    User User = _userService.GetUser(Username, Password);

                    if (User != null)
                    {

                        UserView userView = new UserView(User);
                        var userService = new UserService(new BUMADbContext());
                        userView.DataContext = new UserViewModel(userService, User.AccessLevel);

                        userView.Show();


                        _loginWindow?.Close();
                    }
                    else
                    {
                        ErrorMessage = "Invalid Username or Password!";
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    ErrorMessage = ex.Message; 
                }
            }
            else
            {
                ErrorMessage = "Invalid Username or Password";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
