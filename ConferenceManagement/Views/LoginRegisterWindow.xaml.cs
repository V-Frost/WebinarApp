using System.Windows;
using WebinarApp.Models;
using System.Linq;
using WebinarApp.Helpers;

namespace WebinarApp.Views
{
    public partial class LoginRegisterWindow : Window
    {
        public LoginRegisterWindow()
        {
            InitializeComponent();
            ShowLoginForm();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) => ShowLoginForm();

        private void RegisterButton_Click(object sender, RoutedEventArgs e) => ShowRegisterForm();

        private void ShowLoginForm()
        {
            RepeatPasswordLabel.Visibility = Visibility.Collapsed;
            ConfirmPasswordBox.Visibility = Visibility.Collapsed;
            AdminCodeLabel.Visibility = Visibility.Collapsed;
            AdminCodeTextBox.Visibility = Visibility.Collapsed;
            ConfirmButton.Content = "Вхід";
        }

        private void ShowRegisterForm()
        {
            RepeatPasswordLabel.Visibility = Visibility.Visible;
            ConfirmPasswordBox.Visibility = Visibility.Visible;
            AdminCodeLabel.Visibility = Visibility.Visible;
            AdminCodeTextBox.Visibility = Visibility.Visible;
            ConfirmButton.Content = "Реєстрація";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmButton.Content.ToString() == "Вхід")
                PerformLogin();
            else
                PerformRegistration();
        }

        private void PerformLogin()
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть обидва поля для входу.");
                return;
            }

            using (var context = new AppDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    // Зберігаємо користувача в сеансі
                    Session.CurrentUser = user;

                    // Відображення повідомлення для перевірки
                    MessageBox.Show(user.Role == "Admin" ? "Вхід як адміністратор" : "Вхід як користувач", "Вхід");

                    // Відкриваємо відповідний інтерфейс залежно від ролі
                    Window newWindow;
                    if (user.Role == "Admin")
                    {
                        newWindow = new MainWindow();
                    }
                    else
                    {
                        newWindow = new UserWebinarsWindow();
                    }

                    // Встановлюємо нове вікно як головне
                    Application.Current.MainWindow = newWindow;
                    newWindow.Show();

                    // Закриваємо вікно входу
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Невірний email або пароль", "Помилка входу", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }

        private void PerformRegistration()
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string adminCode = AdminCodeTextBox.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не співпадають.");
                return;
            }

            string role = string.IsNullOrWhiteSpace(adminCode) ? "User" : "Admin";

            var newUser = new Users { Email = email, Password = password, Role = role };
            using (var context = new AppDbContext())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            // Зберігаємо нового користувача в сеансі
            Session.CurrentUser = newUser;
            MessageBox.Show("Реєстрація пройшла успішно!");

            // Автоматично відкриваємо інтерфейс користувача після реєстрації
            Window newWindow;
            if (newUser.Role == "Admin")
            {
                newWindow = new MainWindow();
            }
            else
            {
                newWindow = new UserWebinarsWindow();
            }

            // Встановлюємо нове вікно як головне
            Application.Current.MainWindow = newWindow;
            newWindow.Show();

            // Закриваємо вікно реєстрації
            this.Close();
        }
    }
}



