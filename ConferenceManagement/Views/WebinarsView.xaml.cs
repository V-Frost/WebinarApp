using System.Windows;
using System.Windows.Controls;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class WebinarsView : UserControl
    {
        public WebinarsView()
        {
            InitializeComponent();
            DataContext = new WebinarsViewModel(); // Встановлення ViewModel як DataContext

        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem != null)
            {
                // Логіка для редагування обраного вебінарув
                MessageBox.Show("Редагування вебінару: " + WebinarsDataGrid.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Виберіть вебінар для редагування.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem != null)
            {
                // Логіка для видалення обраного вебінару
                MessageBox.Show("Видалення вебінару: " + WebinarsDataGrid.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Виберіть вебінар для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
