using BUMA.Data;
using BUMA.Models;
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
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private bool _isAdmin;
        private bool _isUser;
        private readonly UserService _userService;
        public ICommand RegisterCommand { get; private set; }
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

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public bool IsUser
        {
            get { return _isUser; }
            set
            {
                _isUser = value;
                OnPropertyChanged();
            }
        }


        public RegisterViewModel(UserService userService)
        {
            RegisterCommand = new RelayCommand(Register);
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        private void Register()
        {

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                if (_isAdmin || _isUser)
                {
                    string accessLevel = _isAdmin ? "Admin" : "User";

                    string passwordHash = ComputeMD5Hash(Password);

                    User newUser = new User
                    {
                        Username = Username,
                        PasswordHash = passwordHash,
                        AccessLevel = accessLevel
                    };

                    bool ExistingUser = _userService.ExistingUser(Username);
                    if (ExistingUser == false)
                    {
                        SaveUserToDatabase(newUser);

                        System.Windows.MessageBox.Show("User registered successfully!");

                        OnRegistrationSuccess();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Username Already in use.");
                        return;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please select an access level: Admin or User.");
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please fill in all fields.");
                return;
            }
          
        }

        public event Action RegistrationSuccess;

        private void OnRegistrationSuccess()
        {
            RegistrationSuccess?.Invoke();
        }

        private void SaveUserToDatabase(User user)
        {
            try
            {
                using (var context = new BUMADbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private string ComputeMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
