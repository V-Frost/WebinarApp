using System.Windows;
using System.Windows.Navigation;
using WebinarApp.Views;
using WebinarApp.Helpers; // Додаємо простір імен для доступу до Session

namespace WebinarApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Перевірка ролі користувача та приховування кнопок для звичайного користувача
            if (Session.UserRole == "User")
            {
                NavigateToParticipantsButton.Visibility = Visibility.Collapsed;
                NavigateToSpeakersButton.Visibility = Visibility.Collapsed;
                NavigateToRegistrationsButton.Visibility = Visibility.Collapsed;
            }
        }

        private void NavigateToWebinars(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new WebinarsView());
        }

        private void NavigateToSpeakers(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SpeakersView());
        }

        private void NavigateToParticipants(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ParticipantsView());
        }

        private void NavigateToRegistrations(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new WebinarRegistrationsView());
        }
    }
}
