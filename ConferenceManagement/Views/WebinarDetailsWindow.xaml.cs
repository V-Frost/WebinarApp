using System;
using System.Windows;
using System.Windows.Threading;
using WebinarApp.Models;

namespace WebinarApp.Views
{
    public partial class WebinarDetailsWindow : Window
    {
        private readonly DispatcherTimer _timer;

        public WebinarDetailsWindow(Webinar webinar)
        {
            InitializeComponent();
            DataContext = webinar;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            _timer.Tick += (s, e) => UpdateTimeUntilWebinar(webinar);
            _timer.Start();

            // Відразу оновлюємо час до початку
            UpdateTimeUntilWebinar(webinar);
        }

        private void UpdateTimeUntilWebinar(Webinar webinar)
        {
            var timeSpan = webinar.Date + webinar.StartTime - DateTime.Now;
            TimeUntilWebinarText.Text = timeSpan.TotalSeconds <= 0
                ? "Вебінар вже розпочався або завершився"
                : timeSpan.TotalDays >= 1
                    ? $"{(int)timeSpan.TotalDays} день {timeSpan.Hours} годин {timeSpan.Minutes} хвилин"
                    : $"{timeSpan.Hours} годин {timeSpan.Minutes} хвилин";
        }
    }
}
