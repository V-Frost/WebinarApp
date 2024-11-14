using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WebinarApp.Models;
using System.ComponentModel;

namespace WebinarApp.ViewModels
{
    public class UserWebinarsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Webinar> Webinars { get; set; } // Усі вебінари
        public ObservableCollection<Webinar> FilteredWebinars { get; set; } // Відфільтровані вебінари

        private string _selectedCategory;
        public ObservableCollection<string> Categories { get; set; } // Категорії для фільтрації

        private string selectedCategory;
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                ApplyFilters(); // Викликаємо фільтрацію при зміні категорії
            }
        }


        public string CurrentWeekText => $"Тиждень: {currentWeekStart:dd.MM} - {currentWeekEnd:dd.MM}";

        private DateTime currentWeekStart;
        private DateTime currentWeekEnd;

        public ICommand PreviousWeekCommand { get; }
        public ICommand NextWeekCommand { get; }

        public UserWebinarsViewModel()
        {
            LoadWebinars();
            LoadCategories();
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
            // Отримуємо всі унікальні категорії з вебінарів
            var categoriesList = Webinars.Select(w => w.Category).Distinct().ToList();

            // Додаємо пункт "Всі вебінари" на початку списку
            categoriesList.Insert(0, "Всі вебінари");

            // Ініціалізуємо Categories з оновленим списком
            Categories = new ObservableCollection<string>(categoriesList);

            // Встановлюємо "Всі вебінари" як обрану категорію за замовчуванням
            SelectedCategory = "Всі вебінари";

            OnPropertyChanged(nameof(Categories));
            OnPropertyChanged(nameof(SelectedCategory));
        }


        private void SetCurrentWeek(DateTime referenceDate)
        {
            currentWeekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek);
            currentWeekEnd = currentWeekStart.AddDays(6);
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

        public void ApplyFilters()
        {
            if (SelectedCategory == "Всі вебінари")
            {
                // Показуємо всі вебінари за обраний тиждень
                FilteredWebinars = new ObservableCollection<Webinar>(
                    Webinars.Where(w => w.Date >= currentWeekStart && w.Date <= currentWeekEnd)
                );
            }
            else
            {
                // Фільтруємо вебінари за обраною категорією і тижнем
                FilteredWebinars = new ObservableCollection<Webinar>(
                    Webinars.Where(w => w.Date >= currentWeekStart && w.Date <= currentWeekEnd &&
                                        w.Category == SelectedCategory)
                );
            }

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
