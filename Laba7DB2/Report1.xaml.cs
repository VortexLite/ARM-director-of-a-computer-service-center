﻿using System;
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
    /// Логика взаимодействия для Report1.xaml
    /// </summary>
    public partial class Report1 : Window
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Report1()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
        }

        private void BtnReport1(object sender, RoutedEventArgs e)
        {
            if (dbconnection.Connect("sa", "qwerty"))
            {
                connection = dbconnection.GetConnection();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ClientOrdersView", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
