using BUMA.Data;
using BUMA.ViewModels;
using BUMA.Views;
using System.Windows;
using System.Windows.Controls;

namespace YourApp.Views
{
    public partial class LoginView : Window
    {
        private LoginViewModel _viewModel;
        public LoginView()
        {
            InitializeComponent();

            var userService = new UserService(new BUMADbContext());
            var viewModel = new LoginViewModel(userService);
            viewModel.SetWindowInstance(this);
            this.DataContext = viewModel; 

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var viewModel = (LoginViewModel)this.DataContext;
            viewModel.Password = passwordBox.Password; 
        }
    }
}