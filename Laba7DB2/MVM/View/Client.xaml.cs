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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba7DB2.MVM.View
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : UserControl
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public Client()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
            connection = dbconnection.GetConnection();
        }

        public void StartHide()
        {
            HideVisualChildren<Label>(this);
            HideVisualChildren<TextBox>(this);
            HideVisualChildren<Line>(this);
            HideVisualChildren<Button>(this);
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
                        DeleteButtonClient.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ShowVisualChildren<T>(child);
                    }
                }
            }
        }

        private void ADD_Client_Click(object sender, RoutedEventArgs e)
        {
            ShowVisualChildren<Label>(this);
            ShowVisualChildren<TextBox>(this);
            ShowVisualChildren<Line>(this);
            ShowVisualChildren<Button>(this);
            DeleteButtonClient.Visibility = Visibility.Collapsed;
            DeleteComboboxClient.Visibility = Visibility.Collapsed;
        }

        private void ADDClient(string id, string name, string surname,
            string middlename, string email, string phone,
            string adress)
        {
            try
            {
                if (!(string.IsNullOrEmpty(id)
                && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(middlename)
                && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone)
                && string.IsNullOrEmpty(adress)))
                {
                    var cmd = new SqlCommand("AddClient", connection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@middlename", middlename);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@adress", adress);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Працівник добавлений", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            string id, name, surname, middlename, email, phone, adress;

            var command = new SqlCommand("SELECT MAX(CAST(ID_Client AS INT)) AS max_id FROM Client", connection);
            try
            {
                id = Convert.ToString(Convert.ToInt32(command.ExecuteScalar().ToString()) + 1);
            }
            catch
            {
                id = "1";
            }

            name = NameClient.Text;
            surname = SurnameClient.Text;
            middlename = MiddleNameClient.Text;
            email = EmailClient.Text;
            phone = PhoneClient.Text;
            adress = AdressClient.Text;

            ADDClient(id, name, surname, middlename, email, phone, adress);
        }

        private void HideAll_Loaded(object sender, RoutedEventArgs e)
        {
            DeleteButtonClient.Visibility = Visibility.Collapsed;
            DeleteComboboxClient.Visibility = Visibility.Collapsed;
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            StartHide();
            DeleteButtonClient.Visibility = Visibility.Visible;
            DeleteComboboxClient.Visibility = Visibility.Visible;

            DeleteComboboxClient.Items.Clear();
            var cmd = new SqlCommand("SELECT c.Surname_Client, c.Name_Client, c.Middlename_Client, c.Email_Client FROM Client as c", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string email = reader.GetString(3);
                string comboBoxItem = $"{surname} {name} {middledlename} {email}";
                DeleteComboboxClient.Items.Add(comboBoxItem);
            }
            reader.Close();


        }

        private void DeleteButtonClient_Click(object sender, RoutedEventArgs e)
        {
            string combotxt = DeleteComboboxClient.Text;
            string name, surname, middledlename, email, procedureName = "DeleteClient";
            string[] words2 = combotxt.Split(' ');
            surname = words2[0];
            name = words2[1];
            middledlename= words2[2];
            email = words2[3];
            try
            {
                if (DeleteComboboxClient.SelectedItem != null)
                {

                    var cmd = new SqlCommand(procedureName, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Surname", surname);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Middlename", middledlename);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();

                    DeleteComboboxClient.Items.Remove(DeleteComboboxClient.SelectedItem);
                    MessageBox.Show("Користувача успішно видалено з бази даних.", "Успіх", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Сталася помилка. " + ex.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
