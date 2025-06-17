using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UpdateProgressText();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Получаем текущее значение
        double currentValue = IQprogress.Value;
        double newValue = Math.Min(currentValue + 20, IQprogress.Maximum);

        // Создаем анимацию для плавного изменения значения
        DoubleAnimation animation = new DoubleAnimation
        {
            From = currentValue,
            To = newValue,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        // Анимация для кнопки
        ScaleTransform buttonScale = (ScaleTransform)upiq.RenderTransform;
        DoubleAnimation buttonAnimation = new DoubleAnimation(0.9, 1, TimeSpan.FromSeconds(0.2));
        buttonScale.BeginAnimation(ScaleTransform.ScaleXProperty, buttonAnimation);
        buttonScale.BeginAnimation(ScaleTransform.ScaleYProperty, buttonAnimation);

        // Запускаем анимацию прогресс-бара
        IQprogress.BeginAnimation(ProgressBar.ValueProperty, animation);

        // Если достигнут максимум, делаем кнопку неактивной
        if (newValue >= IQprogress.Maximum)
        {
            upiq.IsEnabled = false;
        }

        // Анимация мозга при нажатии
        var brainScale = (ScaleTransform)BrainScale;
        DoubleAnimation brainPulse = new DoubleAnimation(1.2, 1, TimeSpan.FromSeconds(0.3))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        brainScale.BeginAnimation(ScaleTransform.ScaleXProperty, brainPulse);
        brainScale.BeginAnimation(ScaleTransform.ScaleYProperty, brainPulse);

        UpdateProgressText();
    }

    private void UpdateProgressText()
    {
        // Находим TextBlock для отображения процента
        var progressText = this.FindName("ProgressText") as TextBlock;
        if (progressText != null)
        {
            progressText.Text = $"{(int)IQprogress.Value} IQ";
        }
    }
}