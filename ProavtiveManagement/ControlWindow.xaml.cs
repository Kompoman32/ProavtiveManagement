using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProavtiveManagement
{
    /// <summary>
    /// Логика взаимодействия для ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        private DBModelContext _cont;

        public ControlWindow()
        {
            InitializeComponent();

            _cont = new DBModelContext();
        }

        private void ContorlWIndow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadChart();
            LoadRecomendations();
        }

        private void LoadChart()
        {
            var list = new List<KeyValuePair<DateTime, float>>();

            using (StreamReader sr = new StreamReader("График.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var str = sr.ReadLine().Split('\t');

                    var now = DateTime.Now;

                    list.Add(new KeyValuePair<DateTime, float>(
                        new DateTime(now.Year, now.Month, now.Day, int.Parse(str[0].Split(':')[0]), int.Parse(str[0].Split(':')[1]), 0),
                        float.Parse(str[1])
                    ));
                }
            }

            ((LineSeries)statisticChart.Series[0]).ItemsSource = list;
        }

        private void LoadRecomendations()
        {
            float val = 0;
            var recomendations =_cont.ScenarioSet
                                     .Where(x => x.triggerList
                                                  .Select(t => t.IsTriggered(val))
                                                  .All(b => b)).ToList();

            recomendations.Sort((a1, a2) => a2.Priority.CompareTo(a1));



        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = sender as ComboBox;

            GridRecomendation.IsEnabled = false;
            GridFullManagement.IsEnabled = false;

            if (c.SelectedIndex > 0)
                GridRecomendation.IsEnabled = true;

            if (c.SelectedIndex > 1)
                GridFullManagement.IsEnabled = true;
        }
    }
}
