using System.Windows.Controls;
using WebinarApp.ViewModels;

namespace WebinarApp.Views
{
    public partial class WebinarRegistrationsView : UserControl
    {
        public WebinarRegistrationsView()
        {
            InitializeComponent();
            DataContext = new WebinarRegistrationsViewModel(); // Прив'язуємо ViewModel до DataContext
        }
    }
}
