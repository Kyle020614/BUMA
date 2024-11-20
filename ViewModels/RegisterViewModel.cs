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


        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register()
        {
            // Ensure Username and Password are not empty
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                // Check if either Admin or User is selected
                if (_isAdmin || _isUser)
                {
                    // Determine the Access Level
                    string accessLevel = _isAdmin ? "Admin" : "User";

                    // Hash the Password (using MD5 for this example)
                    string passwordHash = ComputeMD5Hash(Password);

                    // Create a new User object
                    User newUser = new User
                    {
                        Username = Username,
                        PasswordHash = passwordHash,
                        AccessLevel = accessLevel
                    };
                    // Save the user to the database
                    // You would replace this with your database logic
                    SaveUserToDatabase(newUser);

                    System.Windows.MessageBox.Show("User registered successfully!");

                    OnRegistrationSuccess();
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
                // Handle exceptions (e.g., log the error or display a message)
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        // Method to hash the password using MD5
        private string ComputeMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes); // Convert the byte array to a hex string
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
