using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebinarApp.Models;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class UserWebinarsView : Window
    {
        public UserWebinarsView()
        {
            InitializeComponent();
            DataContext = new UserWebinarsViewModel();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UserWebinarsViewModel viewModel)
            {
                viewModel.ApplyFilters();
            }
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem is Webinar selectedWebinar)
            {
                var detailsWindow = new WebinarDetailsWindow(selectedWebinar);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть вебінар.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem is Webinar selectedWebinar)
            {
                DateTime webinarStartTime = selectedWebinar.Date + selectedWebinar.StartTime;

                if (DateTime.Now >= webinarStartTime)
                {
                    MessageBox.Show("Вебінар вже розпочався або завершився, реєстрація недоступна.", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var registrationWindow = new RegistrationWindow();
                    if (registrationWindow.ShowDialog() == true && registrationWindow.IsRegistered)
                    {
                        using (var context = new AppDbContext())
                        {
                            var participant = new Participant
                            {
                                FirstName = registrationWindow.FirstName,
                                LastName = registrationWindow.LastName,
                                Email = registrationWindow.Email,
                                PhoneNumber = registrationWindow.Phone
                            };

                            var registration = new WebinarRegistration
                            {
                                RegistrationDate = DateTime.Now,
                                Status = "Зареєстрований",
                                Participant = participant,
                                WebinarId = selectedWebinar.WebinarId
                            };

                            context.Participants.Add(participant);
                            context.WebinarRegistrations.Add(registration);
                            context.SaveChanges();

                            MessageBox.Show($"Ви успішно зареєстровані на вебінар '{selectedWebinar.Topic}'!", "Реєстрація", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть вебінар.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void WebinarsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UserWebinarsViewModel viewModel)
            {
                viewModel.SelectedWebinar = WebinarsDataGrid.SelectedItem as Webinar;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Ви дійсно бажаєте вийти?", "Вихід", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Close the current window and show the login window
                var loginWindow = new LoginRegisterWindow();
                loginWindow.Show();
                Close();
            }
        }
    }
}
