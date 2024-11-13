using System.Windows;
using WebinarApp.Models;

namespace WebinarApp.Views
{
    public partial class WebinarDetailsWindow : Window
    {
        public WebinarDetailsWindow(Webinar webinar)
        {
            InitializeComponent();
            DataContext = webinar;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Реєстрація успішна!");
            Close();
        }
    }
}
