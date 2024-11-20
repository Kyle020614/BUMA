using BUMA.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BUMA.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;

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
        public ICommand NewUserCommand { get; private set; } // New command

        private void OpenRegisterView()
        {
            // Logic to navigate to the RegisterView
            RegisterView registerView = new RegisterView();
            registerView.Show();

            // Close the current LoginView if needed
            // You might need to close the LoginView window programmatically here
        }

        public LoginViewModel()
        {
            // Correct way to assign RelayCommand, passing a lambda expression
            LoginCommand = new RelayCommand(Login);
            NewUserCommand = new RelayCommand(OpenRegisterView);

        }

        private void Login()
        {
            // Authentication logic here
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)){
                if (AuthenticateUser(Username, Password))
                {
                    // Proceed to next page or show success message
                    ErrorMessage = "Login successful!";
                }
                else
                {
                    // Handle login failure
                    ErrorMessage = "Invalid Username or Password!";
                }
            }
            else
            {
                ErrorMessage = "Invalid Username or Password";
                return;
            }
        }
            

        private bool AuthenticateUser(string username, string password)
        {
            // Example authentication (replace with real logic)
            return username == "test" && password == "password";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
