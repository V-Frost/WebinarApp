using System.Windows;
using System.Windows.Controls;
using WebinarApp.Models;
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
            // Використовуємо WebinarsDataGrid для доступу до обраного елемента
            if (WebinarsDataGrid.SelectedItem is Webinar selectedWebinar)
            {
                var editWindow = new DynamicEditWindow(selectedWebinar); // Передаємо обраний вебінар у вікно редагування
                if (editWindow.ShowDialog() == true)
                {
                    using (var context = new AppDbContext())
                    {
                        context.Entry(selectedWebinar).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    MessageBox.Show("Зміни збережено.", "Редагування", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Виберіть вебінар для редагування.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WebinarsDataGrid.SelectedItem is Webinar selectedWebinar)
            {
                var result = MessageBox.Show($"Ви дійсно хочете видалити вебінар '{selectedWebinar.Topic}'?",
                                             "Видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        context.Webinars.Remove(selectedWebinar);
                        context.SaveChanges();

                        // Оновлення джерела даних після видалення
                        ((WebinarsViewModel)DataContext).Webinars.Remove(selectedWebinar);
                        MessageBox.Show("Вебінар видалено.", "Видалення", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть вебінар для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
