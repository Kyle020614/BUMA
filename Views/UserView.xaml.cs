using BUMA.Data;
using BUMA.Models;
using BUMA.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BUMA.Views
{
    public partial class UserView : Window
    {
        public UserView(User user)
        {
            InitializeComponent();

            var userService = new UserService(new BUMADbContext());
            var viewModel = new UserViewModel(userService, user.AccessLevel); 

            this.DataContext = viewModel; 
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = DataContext as UserViewModel;

            if (viewModel != null && !viewModel.IsAdmin)
            {
                e.Cancel = true;
            }
        }
    }
}