using BUMA.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace YourApp.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();  // Bind to the ViewModel
        }

        // Define the event handler for PasswordChanged
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var viewModel = (LoginViewModel)this.DataContext;
            viewModel.Password = passwordBox.Password; // Update the ViewModel's Password
        }
    }
}