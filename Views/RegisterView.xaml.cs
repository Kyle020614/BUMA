using BUMA.Data;
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


            var userService = new UserService(new BUMADbContext());

            RegisterViewModel viewModel = new RegisterViewModel(userService);
            viewModel.RegistrationSuccess += OnRegistrationSuccess;
            DataContext = viewModel;
        }

        private void OnRegistrationSuccess()
        {
            var loginView = new LoginView();
            loginView.Show();

            this.Close();

           
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var viewModel = (RegisterViewModel)this.DataContext;
            viewModel.Password = passwordBox.Password;
        }
    }
}
