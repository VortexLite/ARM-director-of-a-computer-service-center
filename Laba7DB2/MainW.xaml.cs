using Laba7DB2.MVM.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;

namespace Laba7DB2
{
    /// <summary>
    /// Логика взаимодействия для MainW.xaml
    /// </summary>
    public partial class MainW : Window
    {
        private string _enter;
        private ConnectionDB dbconnection = new ConnectionDB();
        private SqlConnection connection;
        public MainW()
        {
            InitializeComponent();
            connection = dbconnection.GetConnection();
        }

        public MainW(string enter)
        {
            InitializeComponent();
            connection = dbconnection.GetConnection();
            _enter = enter;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var auth = new MainWindow();
            this.Close();
            auth.Show();
        }

        private void HideReport()
        {
            ReportComboBox.Visibility = Visibility.Collapsed;
            ReportStart.Visibility = Visibility.Collapsed;
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            var newViewModel = new Laba7DB2.MVM.View.ViewM();
            this.DataContext = newViewModel;
            CentrText.Content = ViewBtn.Content;
            HideReport();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            var newOrderModel = new Laba7DB2.MVM.View.Order();
            this.DataContext = newOrderModel;
            CentrText.Content = OrderBtn.Content;
            HideReport();
        }

        private void RepairBtn_Click(object sender, RoutedEventArgs e)
        {
            var newRepairModel = new Laba7DB2.MVM.View.Repair();
            this.DataContext = newRepairModel;
            CentrText.Content = RepairBtn.Content;
            HideReport();
        }

        private void ControlWorker_Click(object sender, RoutedEventArgs e)
        {
            var newControlWorkerModel = new Laba7DB2.MVM.View.ControlsWorker();
            this.DataContext = newControlWorkerModel;
            CentrText.Content = ControlBtn.Content;
            HideReport();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            var newClientModel = new Laba7DB2.MVM.View.Client();
            this.DataContext = newClientModel;
            CentrText.Content = ClientBtn.Content;
            HideReport();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ReportComboBox.Visibility = Visibility.Visible;
            ReportStart.Visibility = Visibility.Visible;
            CentrText.Content = ReportBtn.Content;
        }

        private void StackPanelka_Loaded(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("SELECT COUNT(*) FROM information_schema.table_privileges WHERE grantee = @enter AND table_name = 'Worker';", connection);
            command.Parameters.AddWithValue("@enter", _enter);
            int count = (int)command.ExecuteScalar();

            if (count == 1)
            {
                ControlBtn.Visibility = Visibility.Collapsed;
                ReportBtn.Visibility = Visibility.Collapsed;
                ClientBtn.Margin = new Thickness(0, 10, 0, 60);
            }
        }

        private void ESC_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                var newEmptyWindow = new Laba7DB2.MVM.View.EmptyWindow();
                this.DataContext = newEmptyWindow;
                HideReport();
            }
        }

        private void ReportStart_Click(object sender, RoutedEventArgs e)
        {
            if (ReportComboBox.SelectedItem != null)
            {
                string rep = ReportComboBox.Text;
                switch (rep)
                {
                    case "Звіт про клієнтів із замовленнями":
                        var report1 =  new Report1();
                        report1.Show();
                        break;
                    case "Звіт про оцінку роботи":
                        var report2 = new Report2();
                        report2.Show();
                        break;
                    case "Звіт про запчастини":
                        var report3 = new Report3();
                        report3.Show();
                        break;
                }
            }
        }
    }
}
