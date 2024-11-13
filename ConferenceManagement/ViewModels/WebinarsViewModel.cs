using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WebinarApp.Models;
using System.Data.Entity; 

namespace WebinarApp.ViewModels
{
    public class WebinarsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Webinar> _webinars;

        public ObservableCollection<Webinar> Webinars
        {
            get => _webinars;
            set
            {
                _webinars = value;
                OnPropertyChanged();
            }
        }

        public WebinarsViewModel()
        {
            LoadWebinars();
        }

        private void LoadWebinars()
        {
            using (var context = new AppDbContext())
            {
                Webinars = new ObservableCollection<Webinar>(
                    context.Webinars
                           .Include(w => w.Speaker) // Додаємо Include для завантаження даних про спікера
                           .ToList()
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
