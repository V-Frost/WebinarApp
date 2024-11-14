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
            LoginButton.Style = (Style)FindResource("ActiveButtonStyle");
            RegisterButton.Style = (Style)FindResource("InactiveButtonStyle");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRegisterForm();
            RegisterButton.Style = (Style)FindResource("ActiveButtonStyle");
            LoginButton.Style = (Style)FindResource("InactiveButtonStyle");
        }

        private void ShowLoginForm()
        {
            RepeatPasswordLabel.Visibility = Visibility.Collapsed;
            ConfirmPasswordBox.Visibility = Visibility.Collapsed;
            AdminCodeLabel.Visibility = Visibility.Collapsed;
            AdminCodeTextBox.Visibility = Visibility.Collapsed;
            ConfirmButton.Content = "Вхід";
            Height = 400;
        }

        private void ShowRegisterForm()
        {
            RepeatPasswordLabel.Visibility = Visibility.Visible;
            ConfirmPasswordBox.Visibility = Visibility.Visible;
            AdminCodeLabel.Visibility = Visibility.Visible;
            AdminCodeTextBox.Visibility = Visibility.Visible;
            ConfirmButton.Content = "Реєстрація";
            Height = 450;
            ContentPanel.Margin = new Thickness(0, 80, 0, 0);
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
                    Session.CurrentUser = user;
                    MessageBox.Show(user.Role == "Admin" ? "Вхід як адміністратор" : "Вхід як користувач", "Вхід");

                    Window newWindow;
                    if (user.Role == "Admin")
                    {
                        newWindow = new MainWindow();
                    }
                    else
                    {
                        // Використовуємо UserWebinarsView як вікно
                        newWindow = new UserWebinarsView();
                    }

                    Application.Current.MainWindow = newWindow;
                    newWindow.Show();
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
            string adminCode = AdminCodeTextBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не співпадають.");
                return;
            }

            using (var context = new AppDbContext())
            {
                if (context.Users.Any(u => u.Email == email))
                {
                    MessageBox.Show("Користувач з такою електронною адресою вже зареєстрований.");
                    return;
                }

                string role;
                if (adminCode == "Paton1932*112")
                {
                    role = "Admin";
                }
                else if (string.IsNullOrWhiteSpace(adminCode))
                {
                    role = "User";
                }
                else
                {
                    MessageBox.Show("Невірний код адміністратора.");
                    return;
                }

                var newUser = new Users { Email = email, Password = password, Role = role };
                context.Users.Add(newUser);
                context.SaveChanges();

                Session.CurrentUser = newUser;
                MessageBox.Show("Реєстрація пройшла успішно!");

                Window newWindow;
                if (newUser.Role == "Admin")
                {
                    newWindow = new MainWindow();
                }
                else
                {
                    newWindow = new UserWebinarsView();
                }

                Application.Current.MainWindow = newWindow;
                newWindow.Show();
                this.Close();
            }
        }
    }
}
