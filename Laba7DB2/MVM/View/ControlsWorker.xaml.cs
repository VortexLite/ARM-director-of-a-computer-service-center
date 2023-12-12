using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для ControlsWorker.xaml
    /// </summary>
    public partial class ControlsWorker : UserControl
    {
        private ConnectionDB dbconnection;
        private SqlConnection connection;
        public ControlsWorker()
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
                        DeleteButtonWorker.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ShowVisualChildren<T>(child);
                    }
                }
            }
        }

        private void ADD_Worker(object sender, RoutedEventArgs e)
        {
            ShowVisualChildren<Label>(this);
            ShowVisualChildren<TextBox>(this);
            ShowVisualChildren<Line>(this);
            ShowVisualChildren<Button>(this);
            DeleteButtonWorker.Visibility = Visibility.Collapsed;
            DeleteComboboxWorker.Visibility = Visibility.Collapsed;
        }

        private void ADDWorker(string id, string name, string surname,
            string middlename, string email, string phone,
            string dateBirth, string dateEmployment)
        {
            try
            {
                if (!((string.IsNullOrEmpty(NameWorker.Text)
                && string.IsNullOrEmpty(SurnameWorker.Text) && string.IsNullOrEmpty(MiddleNameWorker.Text)
                && string.IsNullOrEmpty(EmailWorker.Text) && string.IsNullOrEmpty(PhoneWorker.Text)
                && string.IsNullOrEmpty(DataBirthWorker.Text) && string.IsNullOrEmpty(DataEmploymentWorker.Text))))
                {
                    var cmd = new SqlCommand("AddWorker", connection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@id_pos", "2");
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@middlename", middlename);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@dateBirth", dateBirth);
                    cmd.Parameters.AddWithValue("@dateEmployment", dateEmployment);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Працівник добавлений", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddWorker_Click(object sender, RoutedEventArgs e)
        {
            string id, name, surname, middlename, email, phone, dateBirth, dateEmployment;

            var command = new SqlCommand("SELECT MAX(CAST(ID_Worker AS INT)) AS max_id FROM Worker", connection);
            try
            {
                id = Convert.ToString(Convert.ToInt32(command.ExecuteScalar().ToString()) + 1);
            }
            catch
            {
                id = "1";
            }

            name = NameWorker.Text;
            surname = SurnameWorker.Text;
            middlename = MiddleNameWorker.Text;
            email = EmailWorker.Text;
            phone = PhoneWorker.Text;
            dateBirth = DataBirthWorker.Text;
            dateEmployment = DataEmploymentWorker.Text;

            ADDWorker(id, name, surname, middlename, email, phone, dateBirth, dateEmployment);
        }
        private void HideAll_Loaded(object sender, RoutedEventArgs e)
        {
            DeleteButtonWorker.Visibility = Visibility.Collapsed;
            DeleteComboboxWorker.Visibility = Visibility.Collapsed;

        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            StartHide();
            DeleteButtonWorker.Visibility = Visibility.Visible;
            DeleteComboboxWorker.Visibility = Visibility.Visible;

            DeleteComboboxWorker.Items.Clear();
            var cmd = new SqlCommand("SELECT w.Surname_Worker, w.Name_Worker, p.Name_Position \r\nFROM Worker as w \r\nJOIN Position as p ON w.ID_Position = p.ID_Position", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string position = reader.GetString(2);
                string comboBoxItem = $"{surname} {name} {position}";
                DeleteComboboxWorker.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void DeleteButtonWorker_Click(object sender, RoutedEventArgs e)
        {
            string combotxt = DeleteComboboxWorker.Text;
            string[] words = combotxt.Split(' ');
            string name, surname;
            surname = words[0];
            name = words[1];

            try
            {
                if (DeleteComboboxWorker.SelectedItem != null)
                {
                
                    var cmd = new SqlCommand("DELETE FROM Worker\r\nWHERE Surname_Worker = @surname\r\nAND Name_Worker = @name", connection);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.ExecuteNonQuery();

                    DeleteComboboxWorker.Items.Remove(DeleteComboboxWorker.SelectedItem);
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
