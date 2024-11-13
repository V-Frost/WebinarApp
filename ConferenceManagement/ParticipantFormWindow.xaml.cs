using System.Windows;
using WebinarApp.Models;

namespace WebinarApp.Views
{
    public partial class ParticipantFormWindow : Window
    {
        public Participant NewParticipant { get; set; }

        public ParticipantFormWindow(Participant participant = null, bool isEdit = false)
        {
            InitializeComponent();

            // Якщо це редагування, використайте наданого учасника
            NewParticipant = participant ?? new Participant();

            DataContext = NewParticipant; // Встановлює об'єкт як контекст даних

            this.Title = isEdit ? "Редагування" : "Новий учасник";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
