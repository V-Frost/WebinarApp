using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebinarApp.Models;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class UserWebinarsView : UserControl
    {
        public UserWebinarsView()
        {
            InitializeComponent();
            DataContext = new UserWebinarsViewModel(); // Прив’язка до ViewModel
        }

        // Обробник події зміни вибору в ComboBox
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UserWebinarsViewModel viewModel)
            {
                viewModel.ApplyFilters();
            }
        }

        // Обробник події подвійного кліку на таблиці DataGrid
        private void WebinarsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem is Webinar selectedWebinar)
            {
                var detailsWindow = new WebinarDetailsWindow(selectedWebinar);
                detailsWindow.ShowDialog();
            }
        }
    }
}
