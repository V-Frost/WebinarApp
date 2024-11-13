using System.Windows.Controls;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class SpeakersView : UserControl
    {
        public SpeakersView()
        {
            InitializeComponent();
            DataContext = new SpeakersViewModel(); // Прив'язуємо ViewModel до DataContext
        }
    }
}
