using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherMeasure;

namespace WeatherMeasure
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherData weatherData;
        public MainWindow()
        {
            InitializeComponent();
            weatherData = new WeatherData();
            weatherData.measurementsChanged += this.updateLabels;
            weatherData.refreshData(false);
        }
        void updateLabels()
        {
            TemperatureLabel.Content = weatherData.getTemperature();
            HumidityLabel.Content = weatherData.getHumidity();
            PressureLabel.Content = weatherData.getPressure();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            weatherData.refreshData(true);
        }
    }
}
