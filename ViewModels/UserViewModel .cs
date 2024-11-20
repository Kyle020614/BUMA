using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUMA.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using BUMA.Models;

    public class UserViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }

        // Commands for the View
        public ICommand LoadUsersCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserViewModel()
        {
            // Initialize data and commands
            Users = new ObservableCollection<User>();
            LoadUsers();
        }

        // Load users from the database
        private void LoadUsers()
        {
            // Logic to load users from the database
        }

        private void AddUser() { /* Add user to database */ }
        private void EditUser() { /* Edit user in database */ }
        private void DeleteUser() { /* Delete user from database */ }
    }
}
