using System.Windows.Controls;
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
    }
}
