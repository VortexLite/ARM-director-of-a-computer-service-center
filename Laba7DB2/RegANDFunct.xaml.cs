using Laba7DB2.MVM.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Laba7DB2
{
    /// <summary>
    /// Логика взаимодействия для RegANDFunct.xaml
    /// </summary>
    public partial class RegANDFunct : Window
    {
        private ConnectionDB dbconnection;
        private string _login;
        private string _password;
        public RegANDFunct()
        {
            InitializeComponent();
            _login = "sa";
            _password = "qwerty";
            dbconnection = new ConnectionDB();
            
        }
        public RegANDFunct(string login, string password)
        {
            InitializeComponent();
            this._login = login;
            this._password = password;
            dbconnection = new ConnectionDB();
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
                    var cmd = new SqlCommand("AddClient", dbconnection.GetConnection());

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

        /*private void grantperm(ref string surname, ref string name)
        {
            if (dbconnection.Connect("sa", "qwerty"))
            {
                SqlCommand cmd;
                using (cmd = new SqlCommand($"GRANT SELECT ON ProblemCategory TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand($"GRANT SELECT ON ServiceServic TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand($"GRANT SELECT, INSERT ON Orders TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand($"GRANT SELECT ON StatusOrder TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand($"GRANT SELECT, INSERT, DELETE, UPDATE ON JobEvaluation TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand($"GRANT SELECT, INSERT, DELETE, UPDATE ON Feedback TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }
                using (cmd = new SqlCommand($"GRANT SELECT ON Client TO {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }
                using (cmd = new SqlCommand($"GRANT EXECUTE ON dbo.AddOrders {surname}", dbconnection.GetConnection()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }*/

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dbconnection.Connect(_login, _password))
                {
                    string id, name, surname, middlename, email, phone, adress;

                    var command = new SqlCommand("SELECT MAX(CAST(ID_Client AS INT)) AS max_id FROM Client", dbconnection.GetConnection());
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

                    var cmd = new SqlCommand($"CREATE LOGIN {surname} WITH PASSWORD = '{name}'; CREATE USER {surname} FOR LOGIN {surname};", dbconnection.GetConnection());
                    cmd.ExecuteNonQuery();

                    using (cmd = new SqlCommand($"grant select  on TypeSpareParts to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on SpareParts to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on Position to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on Worker to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on Repair to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on ProblemCategory to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on ServiceServic to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select, insert on Orders to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on StatusOrder to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select, insert, delete, update on JobEvaluation to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select on Client to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"grant select, insert, delete, update on Feedback to {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"GRANT EXECUTE ON dbo.AddOrders TO {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"GRANT EXECUTE ON dbo.AddJobEvaluation TO {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (cmd = new SqlCommand($"GRANT EXECUTE ON dbo.AddFeedback TO {surname}", dbconnection.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    /*grantperm(ref surname, ref name);*/

                    var mainwidow = new MainWindow();
                    this.Close();
                    mainwidow.Show();
                }
                
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public void StartHide()
        {
            HideVisualChildren<Label>(this);
            HideVisualChildren<TextBox>(this);
            HideVisualChildren<Line>(this);
            HideVisualChildren<Button>(this);
            xxx.Visibility = Visibility.Visible;
            ViewBtn.Visibility = Visibility.Visible;
            OrderBtn.Visibility = Visibility.Visible;
            FeedbackBtn.Visibility = Visibility.Visible;
            Exit.Visibility = Visibility.Visible;

        }

        public void StartShow()
        {
            ShowVisualChildren<Label>(this);
            ShowVisualChildren<TextBox>(this);
            ShowVisualChildren<Line>(this);
            ShowVisualChildren<Button>(this);
            xxx.Visibility = Visibility.Collapsed;
            ViewBtn.Visibility = Visibility.Collapsed;
            OrderBtn.Visibility = Visibility.Collapsed;
            FeedbackBtn.Visibility = Visibility.Collapsed;
            Exit.Visibility = Visibility.Visible;
            HideFeedback();
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var auth = new MainWindow();
            this.Close();
            auth.Show();
        }

        public void Connects()
        {
           try
            {
                if (dbconnection.Connect(_login, _password))
                {
                    MessageBox.Show("Авторизовано", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
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

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            OrderHide();
            HideFeedback();
            CentrText.Content = ViewBtn.Content + " замовлення";
            OrderDB.Visibility = Visibility.Visible;

            
            var command = new SqlCommand("SELECT ID_Client FROM Client WHERE Client.Surname_Client = @surname AND Client.Name_Client = @name", dbconnection.GetConnection());
            command.Parameters.AddWithValue("@surname", _login);
            command.Parameters.AddWithValue("@name", _password);
            SqlDataReader reader = command.ExecuteReader();
            string id = "";
            while (reader.Read())
            {
                id = reader.GetString(0).Trim();
            }
            reader.Close();

            command = new SqlCommand("SELECT o.ID_Orders, cl.Surname_Client, cl.Name_Client, cl.Middlename_Client, ser.Name_ServiceServic, o.Date_Orders, o.Description_Orders\r\nFROM Orders as o\r\nINNER JOIN Client as cl ON o.ID_Client = cl.ID_Client\r\nINNER JOIN ServiceServic as ser ON o.ID_ServiceServic = ser.ID_ServiceServic\r\nWHERE cl.ID_Client = @id", dbconnection.GetConnection());
            command.Parameters.AddWithValue("@id", id);
            var da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "Orders");
            OrderDB.ItemsSource = ds.Tables["Orders"].DefaultView;
        }

        public void OrderShow()
        {
            LabelClient.Visibility = Visibility.Visible;
            ClientComboBox.Visibility = Visibility.Visible;
            LineOrders.Visibility = Visibility.Visible;

            LabelSevice.Visibility = Visibility.Visible;
            ServicesComboBox.Visibility = Visibility.Visible;
            LineService.Visibility = Visibility.Visible;

            DateOrders.Visibility = Visibility.Visible;
            DateNow.Visibility = Visibility.Visible;
            DateLine.Visibility = Visibility.Visible;

            LabelDesc.Visibility = Visibility.Visible;
            TextBoxOrders.Visibility = Visibility.Visible;
            EnterOrders.Visibility = Visibility.Visible;
        }

        public void OrderHide()
        {
            LabelClient.Visibility = Visibility.Collapsed;
            ClientComboBox.Visibility = Visibility.Collapsed;
            LineOrders.Visibility = Visibility.Collapsed;

            LabelSevice.Visibility = Visibility.Collapsed;
            ServicesComboBox.Visibility = Visibility.Collapsed;
            LineService.Visibility = Visibility.Collapsed;

            DateOrders.Visibility = Visibility.Collapsed;
            DateNow.Visibility = Visibility.Collapsed;
            DateLine.Visibility = Visibility.Collapsed;

            LabelDesc.Visibility = Visibility.Collapsed;
            TextBoxOrders.Visibility = Visibility.Collapsed;
            EnterOrders.Visibility = Visibility.Collapsed;
        }



        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            HideFeedback();
            OrderShow();
            ClientComboBox.Items.Clear();
            var cmd = new SqlCommand("SELECT c.Surname_Client, c.Name_Client, c.Middlename_Client, c.Email_Client FROM Client as c\r\nWHERE c.Surname_Client = @surname AND c.Name_Client = @name", dbconnection.GetConnection());
            cmd.Parameters.AddWithValue("@surname", _login);
            cmd.Parameters.AddWithValue("@name", _password);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string email = reader.GetString(3);
                string comboBoxItem = $"{surname} {name} {middledlename} {email}";
                ClientComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();


            cmd = new SqlCommand("SELECT s.Name_ServiceServic FROM ServiceServic as s", dbconnection.GetConnection());
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                ServicesComboBox.Items.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void EnterOrders_Click(object sender, RoutedEventArgs e)
        {
            string id, client, servic, dates, desc;
            id = client = servic = dates = desc = null;
            SqlCommand cmd;
            SqlDataReader reader;
            if (ClientComboBox.SelectedItem != null)
            {
                string midllename, email;
                string[] words = ClientComboBox.Text.Split(' ');
                midllename = words[2];
                email = words[3];
                cmd = new SqlCommand("SELECT ID_Client FROM Client as c WHERE c.Surname_Client = @surname AND c.Name_Client = @name AND c.Middlename_Client = @middlename AND c.Email_Client = @email", dbconnection.GetConnection());
                cmd.Parameters.AddWithValue("@surname", _login);
                cmd.Parameters.AddWithValue("@name", _password);
                cmd.Parameters.AddWithValue("@middlename", midllename);
                cmd.Parameters.AddWithValue("@email", email);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    client = reader.GetString(0).Trim();
                }
                reader.Close();
            }
            if (ServicesComboBox.SelectedItem != null)
            {
                string name = ServicesComboBox.Text;
                cmd = new SqlCommand("SELECT ID_ServiceServic\r\nFROM ServiceServic\r\nWHERE Name_ServiceServic = @name", dbconnection.GetConnection());
                cmd.Parameters.AddWithValue("@name", name);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    servic = reader.GetString(0).Trim();
                }
                reader.Close();
            }
            dates = (string)DateNow.Content;

            desc = TextBoxOrders.Text;

            cmd = new SqlCommand("SELECT MAX(CAST(ID_Orders AS INT)) AS max_id FROM Orders", dbconnection.GetConnection());
            try
            {
                id = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
            }
            catch
            {
                id = "1";
            }
            

            try
            {
                if (!(string.IsNullOrEmpty(id)
                && string.IsNullOrEmpty(client) && string.IsNullOrEmpty(servic)
                && string.IsNullOrEmpty(dates) && string.IsNullOrEmpty(desc)))
                {
                    cmd = new SqlCommand("AddOrders", dbconnection.GetConnection());

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idOrders", id);
                    cmd.Parameters.AddWithValue("@idClient", client);
                    cmd.Parameters.AddWithValue("@idService", servic);
                    cmd.Parameters.AddWithValue("@idStatus", "1");
                    cmd.Parameters.AddWithValue("@idDate", dates);
                    cmd.Parameters.AddWithValue("@idDesc", desc);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Замовлення добавлене", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DateNow_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateNow.Content = now.ToString("yyyy-MM-dd");
        }

        private void FeedbackBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDataGridsInGrid(GridVis);
            OrderHide();
            ShowFeedback();
        }

        string idTempIdClient;

        private void ShowFeedback()
        {
            FeedbackLabel.Visibility = Visibility.Visible;
            FeedbackCombobox.Visibility = Visibility.Visible;

            DateFeedbeckLabel.Visibility = Visibility.Visible;
            DateFeedbeckTextBox.Visibility = Visibility.Visible;
            DateFeedbeckLine.Visibility = Visibility.Visible;

            EvaluationLabel.Visibility = Visibility.Visible;
            EvaluationTextBox.Visibility = Visibility.Visible;
            EvaluationLine.Visibility = Visibility.Visible;

            CommentsLabel.Visibility = Visibility.Visible;
            CommentsLine.Visibility = Visibility.Visible;
            CommentsTextBox.Visibility = Visibility.Visible;

            ClientFeedbackLabel.Visibility = Visibility.Visible;
            ClientFeedbackComboBox.Visibility = Visibility.Visible;
            ClientFeedbackLine.Visibility = Visibility.Visible;

            ButtonAddFeedback.Visibility = Visibility.Visible;
        }

        private void HideFeedback()
        {
            FeedbackLabel.Visibility = Visibility.Collapsed;
            FeedbackCombobox.Visibility = Visibility.Collapsed;

            DateFeedbeckLabel.Visibility = Visibility.Collapsed;
            DateFeedbeckTextBox.Visibility = Visibility.Collapsed;
            DateFeedbeckLine.Visibility = Visibility.Collapsed;

            EvaluationLabel.Visibility = Visibility.Collapsed;
            EvaluationTextBox.Visibility = Visibility.Collapsed;
            EvaluationLine.Visibility = Visibility.Collapsed;

            CommentsLabel.Visibility = Visibility.Collapsed;
            CommentsLine.Visibility = Visibility.Collapsed;
            CommentsTextBox.Visibility = Visibility.Collapsed;

            ClientFeedbackLabel.Visibility = Visibility.Collapsed;
            ClientFeedbackComboBox.Visibility = Visibility.Collapsed;
            ClientFeedbackLine.Visibility = Visibility.Collapsed;

            ButtonAddFeedback.Visibility = Visibility.Collapsed;
        }

        private void ClientFeedbackComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ClientFeedbackComboBox.Items.Clear();

            var cmd = new SqlCommand("SELECT c.Surname_Client, c.Name_Client, c.Middlename_Client, c.Email_Client, c.ID_Client FROM Client as c WHERE c.Surname_Client = @surname AND c.Name_Client = @name", dbconnection.GetConnection());
            cmd.Parameters.AddWithValue("@surname", _login);
            cmd.Parameters.AddWithValue("@name", _password);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                string surname = reader.GetString(0);
                string name = reader.GetString(1);
                string middledlename = reader.GetString(2);
                string email = reader.GetString(3);
                idTempIdClient = reader.GetString(4);
                string comboBoxItem = $"{surname} {name} {middledlename} {email}";
                ClientFeedbackComboBox.Items.Add(comboBoxItem);
            }
            reader.Close();
        }

        private void DateFeedbeckTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateFeedbeckTextBox.Content = now.ToString("yyyy-MM-dd");
        }

        private void ButtonAddFeedback_Click(object sender, RoutedEventArgs e)
        {
            string idJobEvaluation, idFeedback;

            var cmd = new SqlCommand("SELECT MAX(CAST(ID_JobEvaluation AS INT)) AS max_id FROM JobEvaluation", dbconnection.GetConnection());
            try
            {
                idJobEvaluation = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
            }
            catch
            {
                idJobEvaluation = "1";
            }

            cmd = new SqlCommand("AddJobEvaluation", dbconnection.GetConnection());

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_JobEvaluation", idJobEvaluation);
            cmd.Parameters.AddWithValue("@EvaluationWork", EvaluationTextBox.Text);
            cmd.Parameters.AddWithValue("@Comments_JobEvaluation", CommentsTextBox.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("SELECT MAX(CAST(ID_Feedback AS INT)) AS max_id FROM Feedback", dbconnection.GetConnection());
            try
            {
                idFeedback = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
            }
            catch
            {
                idFeedback = "1";
            }
            string dateFeedback = (string)DateFeedbeckTextBox.Content;
            cmd = new SqlCommand("AddFeedback", dbconnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_Feedback", idFeedback);
            cmd.Parameters.AddWithValue("@ID_Client", idTempIdClient);
            cmd.Parameters.AddWithValue("@ID_JobEvaluation", idJobEvaluation);
            cmd.Parameters.AddWithValue("@Date_Feedback", dateFeedback);
            cmd.ExecuteNonQuery();


            MessageBox.Show("Відгук дан", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FeedbackCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            if (dbconnection.Connect("sa", "qwerty"))
            {
                var command = new SqlCommand("SELECT ID_Client FROM Client WHERE Client.Surname_Client = @surname AND Client.Name_Client = @name", dbconnection.GetConnection());
                command.Parameters.AddWithValue("@surname", _login);
                command.Parameters.AddWithValue("@name", _password);
                SqlDataReader reader = command.ExecuteReader();
                string id = "";
                while (reader.Read())
                {
                    id = reader.GetString(0).Trim();
                }
                reader.Close();

                var cmd = new SqlCommand("SELECT cl.Surname_Client, cl.Name_Client, ser.Name_ServiceServic, o.Date_Orders, o.Description_Orders\r\nFROM Orders as o\r\nINNER JOIN Client as cl ON o.ID_Client = cl.ID_Client\r\nINNER JOIN ServiceServic as ser ON o.ID_ServiceServic = ser.ID_ServiceServic\r\nWHERE cl.ID_Client = @id\r\nAND o.ID_StatusOrder = '3'", dbconnection.GetConnection());
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string surname = reader.GetString(0);
                    string name = reader.GetString(1);
                    string servic = reader.GetString(2);
                    string dateOrders = reader.GetDateTime(3).ToString();
                    string desc = reader.GetString(4);
                    string comboBoxItem = $"{surname} {name} {servic} {dateOrders} {desc}";
                    FeedbackCombobox.Items.Add(comboBoxItem);
                }
                reader.Close();
            }
            
        }
    }
}
