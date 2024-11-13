using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class ParticipantsView : UserControl
    {
        public ParticipantsView()
        {
            InitializeComponent();
            DataContext = new ParticipantsViewModel(); // Прив'язуємо ViewModel до DataContext

        }

        private void ParticipantsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Перевіряємо, чи натиснута клавіша Delete
            if (e.Key == Key.Delete)
            {
                // Отримуємо DataContext і перевіряємо на наявність команди DeleteCommand
                if (DataContext is ParticipantsViewModel viewModel && viewModel.DeleteCommand.CanExecute(null))
                {
                    // Виконуємо команду видалення
                    viewModel.DeleteCommand.Execute(null);
                }
                // Запобігаємо подальшій обробці події Delete у DataGrid
                e.Handled = true;
            }
        }
    }
}
