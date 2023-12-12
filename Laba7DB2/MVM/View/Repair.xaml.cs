using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
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
    /// Логика взаимодействия для Repair.xaml
    /// </summary>
    public partial class Repair : UserControl
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Repair()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
            connection = dbconnection.GetConnection();
        }

        private void ButtonCalendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.SelectedDate += CalendarWindow_SelectedDate;
            calendarWindow.ShowDialog();
        }
        public string ConvertToDateFormat(string text)
        {
            DateTime date;
            bool success = DateTime.TryParseExact(text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (success)
            {
                return date.ToString("yyyy-MM-dd");
            }
            else
            {
                return "Невірний формат дати!";
            }
        }

        private void CalendarWindow_SelectedDate(object sender, DateTime selectedDate)
        {
            DateEndTextBox.Text = ConvertToDateFormat(selectedDate.ToShortDateString());
        }

        private void CategoryComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT Name_ProblemCategory FROM ProblemCategory", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CategoryComboBox.Items.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void SpartsComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            SpartsComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT sp.Name_SpareParts, t.TypeSpareParts\r\nFROM SpareParts as sp\r\nINNER JOIN TypeSpareParts as t \r\nON sp.ID_TypeSpareParts = t.ID_TypeSpareParts", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string typesparts = reader.GetString(0);
                string sparts = reader.GetString(1);

                string comboBoxItem = $"{typesparts} {sparts}";
                SpartsComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void WorkerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            WorkerComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT w.Surname_Worker, w.Name_Worker, w.Middlename_Worker, Position.Name_Position\r\nFROM Worker as w\r\nINNER JOIN Position\r\nON w.ID_Position = Position.ID_Position", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string position = reader.GetString(3);
                string comboBoxItem = $"{surname} {name} {middledlename} {position}";
                WorkerComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void OrdersComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            OrdersComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT cl.Surname_Client, cl.Name_Client, ser.Name_ServiceServic, Date_Orders\r\nFROM Orders\r\nINNER JOIN Client as cl ON Orders.ID_Client = cl.ID_Client\r\nINNER JOIN ServiceServic as ser ON Orders.ID_ServiceServic = ser.ID_ServiceServic\r\nWHERE Orders.ID_StatusOrder != '3'", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string servic = reader.GetString(2);
                string date = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                DateStartLabelOut.Content = date;
                string comboBoxItem = $"{surname} {name} {servic}";
                OrdersComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        public void StartHide()
        {
            HideVisualChildren<Label>(this);
            HideVisualChildren<ComboBox>(this);
            HideVisualChildren<Line>(this);
            HideVisualChildren<Button>(this);
            HideVisualChildren<TextBox>(this);
            Desciption.Visibility = Visibility.Visible;
            TextBoxOrders.Visibility = Visibility.Visible;
            ButtonOrder2.Visibility = Visibility.Visible;

        }
        public void StartShow()
        {
            ShowVisualChildren<Label>(this);
            ShowVisualChildren<ComboBox>(this);
            ShowVisualChildren<Line>(this);
            ShowVisualChildren<Button>(this);
            ShowVisualChildren<TextBox>(this);
            Desciption.Visibility = Visibility.Collapsed;
            TextBoxOrders.Visibility = Visibility.Collapsed;
            ButtonOrder2.Visibility = Visibility.Collapsed;
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
        private void OrdersStart_Loaded(object sender, RoutedEventArgs e)
        {
            StartShow();
        }
        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null &&
                    SpartsComboBox.SelectedItem != null &&
                    WorkerComboBox.SelectedItem != null &&
                    OrdersComboBox.SelectedItem != null &&
                    !string.IsNullOrEmpty((string)DateStartLabelOut.Content) &&
                    !string.IsNullOrEmpty(DateEndTextBox.Text))
            {
                StartHide();
            }
            else
            {
                MessageBox.Show("Не введено всіх даних", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void ButtonOrder2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxOrders.Text))
            {
                string id, idCategory, idSpare = "", idWorker, idOrders, dateStart,
                dateEnd, desc, price, idclient, idServic = "";

                var cmd = new SqlCommand("SELECT MAX(CAST(ID_Repair AS INT)) AS max_id FROM Repair", connection);
                try
                {
                    id = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
                }
                catch
                {
                    id = "1";
                }

                cmd = new SqlCommand("SELECT ID_ProblemCategory\r\nFROM ProblemCategory\r\nWHERE Name_ProblemCategory = @name", connection);
                cmd.Parameters.AddWithValue("@name", CategoryComboBox.SelectedItem);
                idCategory = cmd.ExecuteScalar().ToString();

                string[] words = SpartsComboBox.SelectedItem.ToString().Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    idSpare += words[i] + " ";
                }
                cmd = new SqlCommand("SELECT ID_SpareParts\r\nFROM SpareParts\r\nWHERE Name_SpareParts = @name", connection);
                cmd.Parameters.AddWithValue("@name", idSpare);
                idSpare = cmd.ExecuteScalar().ToString();

                words = WorkerComboBox.SelectedItem.ToString().Split(' ');
                cmd = new SqlCommand("SELECT ID_Worker\r\nFROM Worker\r\nWHERE Surname_Worker = @surname\r\nAND Name_Worker = @name", connection);
                cmd.Parameters.AddWithValue("@surname", words[0]);
                cmd.Parameters.AddWithValue("@name", words[1]);
                idWorker = cmd.ExecuteScalar().ToString();

                words = OrdersComboBox.SelectedItem.ToString().Split(' ');
                cmd = new SqlCommand("SELECT ID_Client\r\nFROM Client\r\nWHERE Surname_Client = @surname\r\nAND Name_Client = @name", connection);
                cmd.Parameters.AddWithValue("@surname", words[0]);
                cmd.Parameters.AddWithValue("@name", words[1]);
                idclient = cmd.ExecuteScalar().ToString();

                words = OrdersComboBox.SelectedItem.ToString().Split(' ');
                for (int i = 2; i < words.Length; i++)
                {
                    idServic += words[i] + " ";
                }
                cmd = new SqlCommand("SELECT ID_ServiceServic\r\nFROM ServiceServic\r\nWHERE Name_ServiceServic = @name", connection);
                cmd.Parameters.AddWithValue("@name", idServic);
                idServic = cmd.ExecuteScalar().ToString();

                dateStart = DateStartLabelOut.Content.ToString();
                dateEnd = DateEndTextBox.Text;

                cmd = new SqlCommand("SELECT ID_Orders\r\nFROM Orders\r\nWHERE ID_Client = @idClient\r\nAND ID_ServiceServic = @idServic\r\nAND Date_Orders = @dateOrders", connection);
                cmd.Parameters.AddWithValue("@idClient", idclient);
                cmd.Parameters.AddWithValue("@idServic", idServic);
                cmd.Parameters.AddWithValue("@dateOrders", dateStart);
                idOrders = cmd.ExecuteScalar().ToString();

                desc = Desciption.Content.ToString();
                string priceServic, priceSpare;

                cmd = new SqlCommand("SELECT Price_ServiceServic\r\nFROM ServiceServic\r\nWHERE ID_ServiceServic = @servic", connection);
                cmd.Parameters.AddWithValue("@servic", idServic);
                priceServic = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand("SELECT Price_SpareParts\r\nFROM SpareParts\r\nWHERE ID_SpareParts = @id", connection);
                cmd.Parameters.AddWithValue("@id", idSpare);
                priceSpare = cmd.ExecuteScalar().ToString();

                price = Convert.ToString(Convert.ToDouble(priceServic) + Convert.ToDouble(priceSpare));

                cmd = new SqlCommand("AddRepair", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Repair", id);
                cmd.Parameters.AddWithValue("@ID_ProblemCategory", idCategory);
                cmd.Parameters.AddWithValue("@ID_SpareParts", idSpare);
                cmd.Parameters.AddWithValue("@ID_Worker", idWorker);
                cmd.Parameters.AddWithValue("@ID_Orders", idOrders);
                cmd.Parameters.AddWithValue("@RepairStartDate", dateStart);
                cmd.Parameters.AddWithValue("@RepairEndDate", dateEnd);
                cmd.Parameters.AddWithValue("@Description_Repair", desc);
                cmd.Parameters.AddWithValue("@Price_Repair", price);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("UPDATE Orders SET ID_StatusOrder = '3' WHERE ID_Orders = @idOrders", connection);
                cmd.Parameters.AddWithValue("@idOrders", idOrders);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Замовлення виконано", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Не заповнено опис", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
