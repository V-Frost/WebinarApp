using System.Windows;

namespace WebinarApp.Views
{
    public partial class RegistrationWindow : Window
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsRegistered { get; private set; } = false;

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // Зберігаємо введені значення
            FirstName = FirstNameTextBox.Text;
            LastName = LastNameTextBox.Text;
            Email = EmailTextBox.Text;
            Phone = PhoneTextBox.Text;

            // Перевірка заповнення обов’язкових полів
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Phone))
            {
                MessageBox.Show("Будь ласка, заповніть усі обов’язкові поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Встановлюємо IsRegistered на true, якщо дані успішно введені
            IsRegistered = true;

            // Показуємо повідомлення про успішну реєстрацію
            MessageBox.Show("Реєстрація успішна!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;  // Закриває вікно і повертає true як результат ShowDialog()
            Close();
        }
    }
}
