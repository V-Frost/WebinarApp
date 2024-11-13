using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WebinarApp.Models;
using System.ComponentModel;
using System.Windows.Controls;
using WebinarApp.Views;

namespace WebinarApp.ViewModels
{
    public class UserWebinarsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Webinar> Webinars { get; set; } // Усі вебінари
        public ObservableCollection<Webinar> FilteredWebinars { get; set; } // Відфільтровані вебінари

        public ObservableCollection<string> Categories { get; set; } // Категорії для фільтрації
        public string SelectedCategory { get; set; } // Обрана категорія

        public string CurrentWeekText => $"Тиждень: {currentWeekStart:dd.MM} - {currentWeekEnd:dd.MM}";

        private DateTime currentWeekStart;
        private DateTime currentWeekEnd;

        public ICommand PreviousWeekCommand { get; }
        public ICommand NextWeekCommand { get; }

        public UserWebinarsViewModel()
        {
            LoadWebinars();
            LoadCategories();

            // Встановлюємо поточний тиждень як значення для фільтра
            SetCurrentWeek(DateTime.Now);

            PreviousWeekCommand = new RelayCommand(ShowPreviousWeek);
            NextWeekCommand = new RelayCommand(ShowNextWeek);
        }

        private void LoadWebinars()
        {
            using (var context = new AppDbContext())
            {
                Webinars = new ObservableCollection<Webinar>(context.Webinars.ToList());
                ApplyFilters();
            }
        }

        private void LoadCategories()
        {
            // Взяти всі унікальні категорії з вебінарів
            Categories = new ObservableCollection<string>(Webinars.Select(w => w.Category).Distinct());
        }

        private void SetCurrentWeek(DateTime referenceDate)
        {
            currentWeekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek); // Початок тижня
            currentWeekEnd = currentWeekStart.AddDays(6); // Кінець тижня
            ApplyFilters();
        }

        private void ShowPreviousWeek()
        {
            SetCurrentWeek(currentWeekStart.AddDays(-7));
        }

        private void ShowNextWeek()
        {
            SetCurrentWeek(currentWeekStart.AddDays(7));
        }

        private void ApplyFilters()
        {
            FilteredWebinars = new ObservableCollection<Webinar>(
                Webinars.Where(w => w.Date >= currentWeekStart && w.Date <= currentWeekEnd &&
                                    (string.IsNullOrEmpty(SelectedCategory) || w.Category == SelectedCategory))
            );
            OnPropertyChanged(nameof(FilteredWebinars));
            OnPropertyChanged(nameof(CurrentWeekText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
