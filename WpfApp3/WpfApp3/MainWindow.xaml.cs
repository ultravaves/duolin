using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckRequiredImages(); // Добавляем проверку изображений при запуске
            IQprogress.Value = 0; // Устанавливаем начальное значение
            UpdateProgressText();
            UpdateComparisonImage(0);
        }

        private void UpdateComparisonImage(double iqValue)
        {
            string imagePath;
            if (iqValue <= 20)
            {
                imagePath = "image/sonofwhore.jpg";
            }
            else if (iqValue > 20 && iqValue <= 50)
            {
                imagePath = "image/sonofwhore.jpg";
            }
            else if (iqValue > 50 && iqValue <= 80)
            {
                imagePath = "image/zolo.png";
            }
            else
            {
                imagePath = "image/genius.jpg";
            }

            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imagePath, UriKind.Relative);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                ComparisonImage.Source = image;

                // Анимация смены изображения
                DoubleAnimation fadeAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.3)
                };
                ComparisonImage.BeginAnimation(Image.OpacityProperty, fadeAnimation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
            }
        }

        private void CheckRequiredImages()
        {
            string[] requiredImages = new[] { "sonofwhore.jpg", "zolo.png", "genius.jpg", "mind-clipart-images-31.png" };
            foreach (var imageName in requiredImages)
            {
                try
                {
                    var uri = new Uri($"/{imageName}", UriKind.Relative);
                    var resourceInfo = Application.GetResourceStream(uri);
                    if (resourceInfo == null)
                    {
                        MessageBox.Show($"Отсутствует изображение: {imageName}\nДобавьте изображение в проект и установите Build Action: Resource");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при проверке изображения {imageName}: {ex.Message}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее значение
            double currentValue = IQprogress.Value;
            double newValue = Math.Min(currentValue + 20, IQprogress.Maximum);

            // Создаем анимацию для плавного изменения значения
            DoubleAnimation progressAnimation = new DoubleAnimation
            {
                From = currentValue,
                To = newValue,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            // Анимация для кнопки
            ScaleTransform buttonTransform = (ScaleTransform)upiq.RenderTransform;
            DoubleAnimation buttonScaleAnimation = new DoubleAnimation
            {
                From = 0.95,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            buttonTransform.BeginAnimation(ScaleTransform.ScaleXProperty, buttonScaleAnimation);
            buttonTransform.BeginAnimation(ScaleTransform.ScaleYProperty, buttonScaleAnimation);

            // Применяем анимацию к прогресс-бару
            IQprogress.BeginAnimation(ProgressBar.ValueProperty, progressAnimation);

            // Анимация всплеска значения для прогресс-бара
            ScaleTransform progressTransform = (ScaleTransform)IQprogress.RenderTransform;
            DoubleAnimation progressScaleAnimation = new DoubleAnimation
            {
                From = 1.05,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            progressTransform.BeginAnimation(ScaleTransform.ScaleYProperty, progressScaleAnimation);

            // Анимация мозга при нажатии
            var brainScale = (ScaleTransform)BrainScale;
            DoubleAnimation brainPulse = new DoubleAnimation
            {
                From = 1.2,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            brainScale.BeginAnimation(ScaleTransform.ScaleXProperty, brainPulse);
            brainScale.BeginAnimation(ScaleTransform.ScaleYProperty, brainPulse);

            // Обновляем изображение сравнения с учетом нового значения
            UpdateComparisonImage(newValue);

            // Если достигнут максимум, делаем кнопку неактивной
            if (newValue >= IQprogress.Maximum)
            {
                upiq.IsEnabled = false;
                MessageBox.Show("Поздравляем! Вы достигли максимального IQ!", "Максимум достигнут", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            UpdateProgressText();
        }

        private void UpdateProgressText()
        {
            int currentIQ = (int)IQprogress.Value;
            string description = GetIQDescription(currentIQ);
            ProgressText.Text = $"{currentIQ} IQ - {description}";
        }

        private string GetIQDescription(int iq)
        {
            if (iq <= 20) return "Абсолютный кретин";
            if (iq <= 50) return "Просто тупой";
            if (iq <= 80) return "Средний";
            if (iq <= 120) return "Выше среднего";
            if (iq <= 160) return "Гений";
            return "Супергений";
        }

        private void Task1_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new Window
            {
                Title = "Математическое задание",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            taskWindow.ShowDialog();
        }

        private void Task2_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new Window
            {
                Title = "Задание на логику",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            taskWindow.ShowDialog();
        }

        private void Task3_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new Window
            {
                Title = "Задание на память",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            taskWindow.ShowDialog();
        }

        public void IncreaseIQ(double amount)
        {
            if (amount <= 0) return;

            double currentValue = IQprogress.Value;
            double newValue = Math.Min(currentValue + amount, IQprogress.Maximum);

            DoubleAnimation progressAnimation = new DoubleAnimation
            {
                From = currentValue,
                To = newValue,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            IQprogress.BeginAnimation(ProgressBar.ValueProperty, progressAnimation);
            UpdateComparisonImage(newValue);
            UpdateProgressText();

            if (newValue >= IQprogress.Maximum)
            {
                upiq.IsEnabled = false;
                MessageBox.Show("Поздравляем! Вы достигли максимального IQ!", "Максимум достигнут", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}