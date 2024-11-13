using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using WebinarApp.Models;

namespace WebinarApp.ViewModels
{
    public class WebinarRegistrationsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WebinarRegistration> _registrations;

        public ObservableCollection<WebinarRegistration> Registrations
        {
            get => _registrations;
            set
            {
                _registrations = value;
                OnPropertyChanged();
            }
        }

        public WebinarRegistrationsViewModel()
        {
            LoadRegistrations();
        }

        private void LoadRegistrations()
        {
            using (var context = new AppDbContext())
            {
                // Завантажуємо реєстрації разом з учасниками та вебінарами
                Registrations = new ObservableCollection<WebinarRegistration>(
                    context.WebinarRegistrations
                           .Include(r => r.Participant)
                           .Include(r => r.Webinar)
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
