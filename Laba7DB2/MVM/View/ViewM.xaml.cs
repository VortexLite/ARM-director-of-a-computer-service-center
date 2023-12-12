using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
using System.Xml;

namespace Laba7DB2.MVM.View
{
    /// <summary>
    /// Логика взаимодействия для ViewM.xaml
    /// </summary>
    public partial class ViewM : UserControl
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public ViewM()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
            connection = dbconnection.GetConnection();
        }
        private string SplitData(string text)
        {
            string[] split = text.Split(' ');
            return split[0] + "\n";
        }
        private void HideDataGridsInGrid(Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is DataGrid)
                {
                    (child as DataGrid).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            WorkerDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM WorkerView", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Worker");
            WorkerDB.ItemsSource = ds.Tables["Worker"].DefaultView;

        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            ClientDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM Client;", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Client");
            ClientDB.ItemsSource = ds.Tables["Client"].DefaultView;
        }

        private void Director_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            DirectorDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM DirectorView", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Worker");
            DirectorDB.ItemsSource = ds.Tables["Worker"].DefaultView;
        }

        private void SpareParts_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            SparePartsDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM SparePartsView", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "SpareParts");
            SparePartsDB.ItemsSource = ds.Tables["SpareParts"].DefaultView;
        }

        private void ServiceServic_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            ServiceServicDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM ServiceServic", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "ServiceServic");
            ServiceServicDB.ItemsSource = ds.Tables["ServiceServic"].DefaultView;
        }

        private void ProblemCategory_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            ProblemCategoryDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM ProblemCategory", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "ProblemCategory");
            ProblemCategoryDB.ItemsSource = ds.Tables["ProblemCategory"].DefaultView;
        }

        private void Repair_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            RepairDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM Repair", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Repair");
            RepairDB.ItemsSource = ds.Tables["Repair"].DefaultView;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            OrderDB.Visibility = Visibility.Visible;
            var command = new SqlCommand($"SELECT * FROM Orders", connection);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Orders");
            OrderDB.ItemsSource = ds.Tables["Orders"].DefaultView;
        }
    }
}
