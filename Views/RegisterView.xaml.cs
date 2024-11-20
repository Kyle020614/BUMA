using BUMA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YourApp.Views;

namespace BUMA.Views
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();

            // Set the ViewModel
            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.RegistrationSuccess += OnRegistrationSuccess; // Subscribe to event
            DataContext = viewModel;
        }

        // Method to handle successful registration and window change
        private void OnRegistrationSuccess()
        {
            // Close the RegisterView window
            this.Close();

            // Open the LoginView window
            var loginView = new LoginView();
            loginView.Show();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var viewModel = (RegisterViewModel)this.DataContext;
            viewModel.Password = passwordBox.Password; // Update the ViewModel's Password
        }
    }
}
