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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginForm();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRegisterForm();
        }

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
            {
                PerformLogin();
            }
            else
            {
                PerformRegistration();
            }
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
                    MessageBox.Show(user.Role == "Admin" ? "Вхід як адміністратор" : "Вхід як користувач", "Вхід");

                    Session.CurrentUser = user;
                    this.DialogResult = true;
                    Close();
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

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Будь ласка, заповніть всі необхідні поля.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не співпадають.");
                return;
            }

            string role = string.IsNullOrWhiteSpace(adminCode) ? "User" : "Admin";

            if (role == "Admin" && adminCode != "Paton1932*112")
            {
                MessageBox.Show("Неправильний код адміністратора.");
                return;
            }

            var newUser = new Users
            {
                Email = email,
                Password = password,
                Role = role
            };

            using (var context = new AppDbContext())
            {
                if (context.Users.Any(u => u.Email == email))
                {
                    MessageBox.Show("Користувач з такою електронною адресою вже існує.");
                    return;
                }

                context.Users.Add(newUser);
                context.SaveChanges();
            }

            Session.CurrentUser = newUser;
            MessageBox.Show("Реєстрація пройшла успішно!");

            this.DialogResult = true;
        }
    }
}
