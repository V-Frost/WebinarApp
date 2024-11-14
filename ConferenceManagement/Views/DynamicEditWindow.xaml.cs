using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebinarApp.Models;

namespace WebinarApp.Views
{
    public partial class DynamicEditWindow : Window
    {
        private object _editingObject;
        private Dictionary<string, Control> _controlsMap = new Dictionary<string, Control>();

        // Словник з українськими перекладами назв властивостей
        private Dictionary<string, string> propertyTranslations = new Dictionary<string, string>
        {
            { "WebinarId", "ID Вебінару" },
            { "Topic", "Тема" },
            { "Date", "Дата" },
            { "StartTime", "Час початку" },
            { "EndTime", "Час завершення" },
            { "Link", "Посилання" },
            { "Status", "Статус" },
            { "Category", "Категорія" },
            { "SpeakerId", "Спікер" }
        };

        public DynamicEditWindow(object editingObject)
        {
            InitializeComponent();
            _editingObject = editingObject;

            GenerateFields();
        }

        private void GenerateFields()
        {
            var properties = _editingObject.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string)) continue;

                string propertyName = propertyTranslations.ContainsKey(prop.Name) ? propertyTranslations[prop.Name] : prop.Name;

                var label = new TextBlock { Text = propertyName, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 5, 0, 0) };
                FieldsPanel.Children.Add(label);

                Control control;

                // Якщо властивість — це ID спікера, використовуємо ComboBox
                if (prop.Name == "SpeakerId" && _editingObject is Webinar)
                {
                    var comboBox = new ComboBox();
                    using (var context = new AppDbContext())
                    {
                        var speakers = context.Speakers.ToList();
                        comboBox.ItemsSource = speakers;
                        comboBox.DisplayMemberPath = "FullName";
                        comboBox.SelectedValuePath = "SpeakerId";
                        comboBox.SelectedValue = prop.GetValue(_editingObject);
                    }
                    control = comboBox;
                }
                else
                {
                    control = new TextBox { Text = prop.GetValue(_editingObject)?.ToString() };
                }

                control.Margin = new Thickness(0, 5, 0, 10);
                _controlsMap[prop.Name] = control;
                FieldsPanel.Children.Add(control);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var properties = _editingObject.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    if (!_controlsMap.ContainsKey(prop.Name)) continue;

                    var control = _controlsMap[prop.Name];
                    object value;

                    if (control is ComboBox comboBox)
                    {
                        value = comboBox.SelectedValue; // Зберігаємо ID спікера
                    }
                    else if (control is TextBox textBox)
                    {
                        value = Convert.ChangeType(textBox.Text, prop.PropertyType);
                    }
                    else
                    {
                        continue;
                    }

                    prop.SetValue(_editingObject, value);
                }

                using (var context = new AppDbContext())
                {
                    // Оновлюємо об'єкт у контексті бази даних і зберігаємо зміни
                    context.Entry(_editingObject).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                MessageBox.Show("Зміни збережено.", "Редагування", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Закриває вікно без аварійного завершення
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
