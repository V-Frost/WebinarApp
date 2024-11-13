using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebinarApp.Models;
using WebinarApp.Views;

namespace WebinarApp.Views
{
    public partial class UserWebinarsView : UserControl
    {
        public UserWebinarsView()
        {
            InitializeComponent();
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
