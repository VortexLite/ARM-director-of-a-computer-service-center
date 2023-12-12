using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Data.Entity;

namespace Laba7DB2
{
    internal class ConnectionDB
    {
        private static SqlConnection connection;
        private string username;
        private string password;

        public ConnectionDB()
        {

        }

        public static string getConnectionFile()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                + "\\DBConnectionString";
        }

        public static string getConnectionString(string connectionString)
        {
            StreamReader rdr = new StreamReader(connectionString);
            string constring = (rdr.ReadToEnd());
            rdr.Close();
            return constring;
        }

        public bool Connect(string username, string password)
        {
            this.username = username;
            this.password = password;
            string connectionString = getConnectionString(getConnectionFile());

            string[] components = connectionString.Split(';');
            string userId = "";
            string pass = "";
            string initialCatalog = "";
            string dataSource = "";
            foreach (string component in components)
            {
                if (component.StartsWith("User ID="))
                {
                    userId = component.Substring("User ID=".Length);
                }
                else if (component.StartsWith("Password="))
                {
                    pass = component.Substring("Password=".Length);
                }
                else if (component.StartsWith("Initial Catalog="))
                {
                    initialCatalog = component.Substring("Initial Catalog=".Length);
                }
                else if (component.StartsWith("Data Source="))
                {
                    dataSource = component.Substring("Data Source=".Length);
                }
            }
            string newConnectionString = $"User ID={this.username};Password={this.password};Initial Catalog={initialCatalog};Data Source={dataSource}";

            // Підключення до бази даних
            connection = new SqlConnection(newConnectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}
