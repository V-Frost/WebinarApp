using System.Windows;
using System.Windows.Controls;
using WebinarApp.Views;

namespace WebinarApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обробники для кнопок навігації
        private void ShowWebinars(object sender, RoutedEventArgs e) => MainContent.Content = new WebinarsView();
        private void ShowSpeakers(object sender, RoutedEventArgs e) => MainContent.Content = new SpeakersView();
        private void ShowParticipants(object sender, RoutedEventArgs e) => MainContent.Content = new ParticipantsView();
        private void ShowRegistrations(object sender, RoutedEventArgs e) => MainContent.Content = new WebinarRegistrationsView();

        // Обробники для контекстного меню
        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MainContent.Content is UserControl currentView &&
                ((DataGrid)currentView.FindName("AdminDataGrid"))?.SelectedItem is var selectedItem && selectedItem != null)
            {
                // Дії редагування для `selectedItem`
                MessageBox.Show($"Редагування: {selectedItem}");
            }
            else
            {
                MessageBox.Show("Виберіть рядок для редагування", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MainContent.Content is UserControl currentView &&
                ((DataGrid)currentView.FindName("AdminDataGrid"))?.SelectedItem is var selectedItem && selectedItem != null)
            {
                // Дії видалення для `selectedItem`
                MessageBox.Show($"Видалення: {selectedItem}");
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
