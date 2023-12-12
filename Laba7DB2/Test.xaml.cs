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
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Test()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (dbconnection.Connect("sa", "qwerty"))
            {
                connection = dbconnection.GetConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ClientOrdersView", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ReportViewerDemo.LocalReport.DataSources.Clear();
                ReportDataSource source = new ReportDataSource("DataSet1", dt);
                ReportViewerDemo.LocalReport.ReportPath = "Report1.rdlc";
                ReportViewerDemo.LocalReport.DataSources.Add(source);

                ReportViewerDemo.RefreshReport();
            }
        }
    }
}
