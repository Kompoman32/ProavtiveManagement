using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using ProavtiveManagement.Model;

namespace ProavtiveManagement
{
    /// <summary>
    /// Логика взаимодействия для ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        private DBModelContext _cont;

        //private Random rnd = new Random();

        public ControlWindow()
        {
            InitializeComponent();

            _cont = new DBModelContext();

            //TODO убрать это 
            int count = _cont.ScenarioSet.ToList().Count;
            if (count == 0 && File.Exists("dbo.TemperatureDatas.data.sql"))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBModelContext.GetConnectingString()))
                    {
                        conn.Open();
                        SqlCommand com = new SqlCommand(File.ReadAllText("dbo.TemperatureDatas.data.sql"),conn);
                        com.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Ошибка:\n{e}");
                }
            }

            LoadRecomendations();
            LoadActions();
        }

        private void ControlWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadChart();
            //LoadRecomendations(Dispatcher);

            //var now = DateTime.Now;
            //var timer = new Timer(LoadRecomendations, Dispatcher,
                //now.Subtract(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)).Milliseconds*0+10
                //100,500);
        }

        private void LoadChart()
        {
            var list = new List<KeyValuePair<DateTime, double>>();

            foreach (var d in _cont.TemperatureDataSet.OrderByDescending(data => data.DateTime))
                list.Add(new KeyValuePair<DateTime, double>(d.DateTime,d.Value));

            ((LineSeries)statisticChart.Series[0]).ItemsSource = list;
        }
        private void LoadRecomendations()
        {
            double val = 0;
            var recomendations = new List<Scenario>();

            var scenarios = _cont.ScenarioSet.ToList();


            foreach (var s in scenarios)
            {
                bool ok = true;

                foreach (var t in s.triggerList)
                    ok &= t.IsTriggered(val);

                if (ok)
                    recomendations.Add(s);
            }

            recomendations.Sort((a1, a2) => a2.Efficiency.CompareTo(a1.Efficiency));

            UpdateStackPanelRecomendations(StackPanelRecomendationsByEfficency, recomendations, true);

            recomendations.Sort((a1, a2) => a2.UseFrequency.CompareTo(a1.UseFrequency));

            UpdateStackPanelRecomendations(StackPanelRecomendationsByUseFrequency, recomendations, false);
        }
        private void LoadActions()
        {
            double val = 0;
            var recomendations = new List<Scenario>();

            var scenarios = _cont.ScenarioSet.ToList();


            foreach (var s in scenarios)
            {
                bool ok = true;

                foreach (var t in s.triggerList)
                    ok &= t.IsTriggered(val);

                if (ok)
                    recomendations.Add(s);
            }

            recomendations.Sort((a1, a2) => a2.Efficiency.CompareTo(a1.Efficiency));

            UpdateStackPanelActions(StackPanelActionsByEfficency, recomendations, true);

            recomendations.Sort((a1, a2) => a2.UseFrequency.CompareTo(a1.UseFrequency));

            UpdateStackPanelActions(StackPanelActionsByUseFrequency, recomendations, false);
        }

        private void UpdateStackPanelRecomendations(StackPanel sp, List<Scenario> list, bool efficiency)
        {
            sp.Children.Clear();

            foreach (var r in list)
            {
                var wp = new WrapPanel();
                wp.Children.Add(new Label()
                {
                    Content = r.ToString(),
                    Width = 150,
                    Height = 26,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                });

                var label = new Label();

                var button = new Button { Content = "Желаете выполнить этот сценарий?", Width = 202, Height = 26 };
                button.Click += (b, e) => { Dispatcher.Invoke(() =>
                {
                    r.UseFrequency++;
                    _cont.SaveChanges();
                    Button_Click(button, label);
                }); };
                wp.Children.Add(button);
                wp.Children.Add(new Label() { Content = efficiency ? $"Эфект. = {r.Efficiency}" : $" Част. = {r.UseFrequency}" });
                wp.Children.Add(label);

                sp.Children.Add(wp);
            }
        }
        private void UpdateStackPanelActions(StackPanel sp, List<Scenario> list, bool efficiency)
        {
            sp.Children.Clear();

            foreach (var r in list)
            {
                var wp = new WrapPanel();
                wp.Children.Add(new Label()
                {
                    Content = r.ToString(),
                    Width = 150,
                    Height = 26,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                });

                wp.Children.Add(new Label(){Content = efficiency?$"Эфект. = {r.Efficiency}": $" Част. = {r.UseFrequency}" });
                wp.Children.Add(new Label());

                sp.Children.Add(wp);
            }

           if (sp.Children.Count>0)
                ((sp.Children[0] as WrapPanel).Children[2] as Label).Content = "Выполняется...";
        }

        private void Button_Click(Button button, Label label)
        {
            label.Content = "Выполняется...";
            button.IsEnabled = false;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            LoadRecomendations();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadActions();
        }
    }
}
