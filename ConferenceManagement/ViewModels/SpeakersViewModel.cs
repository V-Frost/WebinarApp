using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WebinarApp.Models;

namespace WebinarApp.ViewModels
{
    public class SpeakersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Speaker> _speakers;

        public ObservableCollection<Speaker> Speakers
        {
            get => _speakers;
            set
            {
                _speakers = value;
                OnPropertyChanged();
            }
        }

        public SpeakersViewModel()
        {
            LoadSpeakers();
        }

        private void LoadSpeakers()
        {
            using (var context = new AppDbContext())
            {
                Speakers = new ObservableCollection<Speaker>(context.Speakers.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
