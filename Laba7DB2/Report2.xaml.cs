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
    /// Логика взаимодействия для Report2.xaml
    /// </summary>
    public partial class Report2 : Window
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Report2()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
        }

        private void BtnReport2(object sender, RoutedEventArgs e)
        {
            if (dbconnection.Connect("sa", "qwerty"))
            {
                connection = dbconnection.GetConnection();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM JobEvaluationView", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                ReportViewerDemo.LocalReport.DataSources.Clear();
                ReportDataSource source = new ReportDataSource("DataSet2", dt);
                ReportViewerDemo.LocalReport.ReportPath = "Report2.rdlc";
                ReportViewerDemo.LocalReport.DataSources.Add(source);

                ReportViewerDemo.RefreshReport();
            }
        }
    }
}
