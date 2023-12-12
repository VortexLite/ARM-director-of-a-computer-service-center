using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace Laba7DB2.MVM.View
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : UserControl
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Order()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
            connection = dbconnection.GetConnection();
        }

        private void OrderComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            OrderComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT Client.Surname_Client, Client.Name_Client, Client.Middlename_Client, ServiceServic.Name_ServiceServic, Orders.Date_Orders\r\nFROM Orders\r\nINNER JOIN Client ON Orders.ID_Client = Client.ID_Client\r\nINNER JOIN ServiceServic ON Orders.ID_Orders = ServiceServic.ID_ServiceServic\r\nWHERE Orders.ID_StatusOrder != '3'", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string servic = reader.GetString(3);
                string date = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                string comboBoxItem = $"{surname} {name} {middledlename} {servic} {date}";
                OrderComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void WorkerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            WorkerComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT w.Surname_Worker, w.Name_Worker, w.Middlename_Worker, w.Email_Worker\r\nFROM Worker as w", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string servic = reader.GetString(3);
                string comboBoxItem = $"{surname} {name} {middledlename} {servic}";
                WorkerComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxOrders.Clear();
            string surname, name, middlename, servic = "", date;
            string[] words = OrderComboBox.SelectedItem.ToString().Split(' ');
            surname = words[0];
            name = words[1];
            middlename = words[2];
            date = words[words.Length - 1];
            for (int i = 3; i < words.Length - 1; i++)
            {
                servic += words[i] + " ";
            }
            var cmd = new SqlCommand("SELECT Orders.Description_Orders\r\nFROM Orders\r\nINNER JOIN Client ON Orders.ID_Client = Client.ID_Client\r\nINNER JOIN ServiceServic ON Orders.ID_Orders = ServiceServic.ID_ServiceServic\r\nWHERE Client.Surname_Client = @surname AND\r\nClient.Name_Client = @name AND\r\nClient.Middlename_Client = @middlename AND\r\nServiceServic.Name_ServiceServic = @servic AND\r\nOrders.Date_Orders = @date", connection);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@middlename", middlename);
            cmd.Parameters.AddWithValue("@servic", servic);
            cmd.Parameters.AddWithValue("@date", date);

            TextBoxOrders.Text = cmd.ExecuteScalar()?.ToString();
        }

        private void EnterOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surname, name, middlename, servic = "", date, id = "";
                string[] words = OrderComboBox.SelectedItem.ToString().Split(' ');
                surname = words[0];
                name = words[1];
                middlename = words[2];
                date = words[words.Length - 1];
                for (int i = 3; i < words.Length - 1; i++)
                {
                    servic += words[i] + " ";
                }

                var cmd = new SqlCommand("SELECT ID_Orders\r\nFROM Orders\r\nINNER JOIN Client ON Orders.ID_Client = Client.ID_Client\r\nINNER JOIN ServiceServic ON Orders.ID_Orders = ServiceServic.ID_ServiceServic\r\nWHERE Client.Surname_Client = @surname \r\nAND Client.Name_Client = @name \r\nAND Client.Middlename_Client = @middlename \r\nAND ServiceServic.Name_ServiceServic = @servic \r\nAND Orders.Date_Orders = @date", dbconnection.GetConnection());
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@middlename", middlename);
                cmd.Parameters.AddWithValue("@servic", servic);
                cmd.Parameters.AddWithValue("@date", date);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetString(0);
                }
                reader.Close();

                cmd = new SqlCommand("UPDATE Orders SET ID_StatusOrder = '2' WHERE ID_Orders = @id", dbconnection.GetConnection());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Замовлення оновлено", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);


                /*cmd = new SqlCommand("SELECT MAX(CAST(ID_Repair AS INT)) AS max_id FROM Repair", connection);
                string idOrder = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                cmd = new SqlCommand("AddRepair", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Repair", idOrder);
                cmd.Parameters.AddWithValue("@ID_ProblemCategory", name);
                cmd.Parameters.AddWithValue("@ID_SpareParts", surname);
                cmd.Parameters.AddWithValue("@ID_Worker", middlename);
                cmd.Parameters.AddWithValue("@RepairStartDate", email);
                cmd.Parameters.AddWithValue("@RepairEndDate", phone);
                cmd.Parameters.AddWithValue("@Description_Repair", adress);
                cmd.Parameters.AddWithValue("@Price_Repair", adress);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Замовлення підтвердженно", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);*/


            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void StartHide()
        {
            HideVisualChildren<Label>(this);
            HideVisualChildren<ComboBox>(this);
            HideVisualChildren<TextBox>(this);
            HideVisualChildren<Line>(this);
            HideVisualChildren<Button>(this);

            OrderLabel1.Visibility = Visibility.Visible;
            OrderComboBox1.Visibility = Visibility.Visible;
            OrderLine1.Visibility = Visibility.Visible;
            EnterOrders1.Visibility = Visibility.Visible;
        }

        public void StartShow()
        {
            ShowVisualChildren<Label>(this);
            ShowVisualChildren<ComboBox>(this);
            ShowVisualChildren<TextBox>(this);
            ShowVisualChildren<Line>(this);
            ShowVisualChildren<Button>(this);

            OrderLabel1.Visibility = Visibility.Collapsed;
            OrderComboBox1.Visibility = Visibility.Collapsed;
            OrderLine1.Visibility = Visibility.Collapsed;
            EnterOrders1.Visibility = Visibility.Collapsed;
        }

        private static void HideVisualChildren<T>(DependencyObject obj) where T : DependencyObject, new()
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        ((T)child).SetValue(VisibilityProperty, Visibility.Collapsed);
                    }
                    else
                    {
                        HideVisualChildren<T>(child);
                    }
                }
            }
        }

        private void ShowVisualChildren<T>(DependencyObject obj) where T : DependencyObject, new()
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        ((T)child).SetValue(VisibilityProperty, Visibility.Visible);
                    }
                    else
                    {
                        ShowVisualChildren<T>(child);
                    }
                }
            }
        }

        private void AcceptanceOrders(object sender, RoutedEventArgs e)
        {
            StartShow();
        }

        private void RejectionOrder(object sender, RoutedEventArgs e)
        {
            StartHide();

            OrderComboBox1.Items.Clear();
            var cmd = new SqlCommand("SELECT Client.Surname_Client, Client.Name_Client, Client.Middlename_Client, ServiceServic.Name_ServiceServic, Orders.Date_Orders\r\nFROM Orders\r\nINNER JOIN Client ON Orders.ID_Client = Client.ID_Client\r\nINNER JOIN ServiceServic ON Orders.ID_Orders = ServiceServic.ID_ServiceServic\r\nWHERE Orders.ID_StatusOrder != '3'", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string servic = reader.GetString(3);
                string date = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                string comboBoxItem = $"{surname} {name} {middledlename} {servic} {date}";
                OrderComboBox1.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void EnterOrders_Click1(object sender, RoutedEventArgs e)
        {
            string surname, name, middlename, servic = "", date, id = "";
            string[] words = OrderComboBox1.SelectedItem.ToString().Split(' ');
            surname = words[0];
            name = words[1];
            middlename = words[2];
            date = words[words.Length - 1];
            for (int i = 3; i < words.Length - 1; i++)
            {
                servic += words[i] + " ";
            }

            var cmd = new SqlCommand("SELECT ID_Orders\r\nFROM Orders\r\nINNER JOIN Client ON Orders.ID_Client = Client.ID_Client\r\nINNER JOIN ServiceServic ON Orders.ID_Orders = ServiceServic.ID_ServiceServic\r\nWHERE Client.Surname_Client = @surname \r\nAND Client.Name_Client = @name \r\nAND Client.Middlename_Client = @middlename \r\nAND ServiceServic.Name_ServiceServic = @servic \r\nAND Orders.Date_Orders = @date", dbconnection.GetConnection());
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@middlename", middlename);
            cmd.Parameters.AddWithValue("@servic", servic);
            cmd.Parameters.AddWithValue("@date", date);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetString(0);
            }
            reader.Close();

            cmd = new SqlCommand("UPDATE Orders SET ID_StatusOrder = '3' WHERE ID_Orders = @id", dbconnection.GetConnection());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
