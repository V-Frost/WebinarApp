using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WebinarApp.Models;
using WebinarApp.Views;

namespace WebinarApp.ViewModels
{
    public class ParticipantsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Participant> Participants { get; set; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private Participant _selectedParticipant;
        public Participant SelectedParticipant
        {
            get => _selectedParticipant;
            set
            {
                _selectedParticipant = value;
                OnPropertyChanged();
            }
        }

        public ParticipantsViewModel()
        {
            LoadParticipants();
            AddCommand = new RelayCommand(AddParticipant);
            EditCommand = new RelayCommand(EditParticipant, () => SelectedParticipant != null);
            DeleteCommand = new RelayCommand(DeleteParticipant, () => SelectedParticipant != null);
        }

        private void LoadParticipants()
        {
            using (var context = new AppDbContext())
            {
                Participants = new ObservableCollection<Participant>(context.Participants.ToList());
            }
        }

        private void AddParticipant()
        {
            var formWindow = new ParticipantFormWindow();
            if (formWindow.ShowDialog() == true)
            {
                var newParticipant = formWindow.NewParticipant;

                using (var context = new AppDbContext())
                {
                    context.Participants.Add(newParticipant);
                    context.SaveChanges();
                }

                Participants.Add(newParticipant);
            }
        }

        private void EditParticipant()
        {
            if (SelectedParticipant != null)
            {
                // Клонування об'єкта для уникнення змін у разі скасування редагування
                var participantCopy = new Participant
                {
                    ParticipantId = SelectedParticipant.ParticipantId,
                    FirstName = SelectedParticipant.FirstName,
                    LastName = SelectedParticipant.LastName,
                    Email = SelectedParticipant.Email,
                    PhoneNumber = SelectedParticipant.PhoneNumber
                };

                var formWindow = new ParticipantFormWindow(participantCopy, true);
                if (formWindow.ShowDialog() == true)
                {
                    using (var context = new AppDbContext())
                    {
                        var participantToUpdate = context.Participants.Find(SelectedParticipant.ParticipantId);
                        if (participantToUpdate != null)
                        {
                            participantToUpdate.FirstName = formWindow.NewParticipant.FirstName;
                            participantToUpdate.LastName = formWindow.NewParticipant.LastName;
                            participantToUpdate.Email = formWindow.NewParticipant.Email;
                            participantToUpdate.PhoneNumber = formWindow.NewParticipant.PhoneNumber;

                            context.SaveChanges();
                        }
                    }

                    // Оновлюємо дані в вибраному учаснику
                    SelectedParticipant.FirstName = formWindow.NewParticipant.FirstName;
                    SelectedParticipant.LastName = formWindow.NewParticipant.LastName;
                    SelectedParticipant.Email = formWindow.NewParticipant.Email;
                    SelectedParticipant.PhoneNumber = formWindow.NewParticipant.PhoneNumber;
                }
            }
        }

        private void DeleteParticipant()
        {
            if (SelectedParticipant != null)
            {
                MessageBoxResult result = MessageBox.Show("Ви дійсно хочете видалити цього учасника?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        var participantToRemove = context.Participants.Find(SelectedParticipant.ParticipantId);
                        if (participantToRemove != null)
                        {
                            context.Participants.Remove(participantToRemove);
                            context.SaveChanges();
                        }
                    }

                    // Видаляємо учасника з колекції, щоб оновити UI
                    Participants.Remove(SelectedParticipant);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
