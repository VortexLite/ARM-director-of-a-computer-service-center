using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Laba7DB2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnectionDB dbconnection;
        public MainWindow()
        {
            InitializeComponent();
            dbconnection = new ConnectionDB();
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Control_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (dbconnection.Connect(Login.Text, Pass.Text))
            {
                MessageBox.Show("Підключення до бази даних встановлено успішно!", "Успіх", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                var mainw = new MainW(Login.Text);
                this.Close();
                mainw.Show();
            }
            else
            {
                MessageBox.Show("Помилка підключення", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Login_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = string.Empty;
            }
        }

        private void Pass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = string.Empty;
            }
        }

        private void EnterForm(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Enter_Click(sender, e);
            }
        }

        private void Registers_Click(object sender, RoutedEventArgs e)
        {
            
            if (Login.Text == "Логін" || Pass.Text == "Пароль"
                || Login.Text == "" || Pass.Text == "")
            {
                var regaddfunc = new RegANDFunct();
                this.Close();
                regaddfunc.Show();
                regaddfunc.StartShow();
                regaddfunc.OrderHide();
                regaddfunc.Connects();
                regaddfunc.CentrText.Content = "Управління";
                regaddfunc.ViewBtn.Visibility = Visibility.Collapsed;
                regaddfunc.OrderBtn.Visibility = Visibility.Collapsed;
                regaddfunc.OrderBtn.Visibility = Visibility.Collapsed;
                regaddfunc.Exit.Margin = new Thickness(0, 480, 0, 0);
            }
            else
            {
                var regaddfunc = new RegANDFunct(Login.Text, Pass.Text);
                regaddfunc.Connects();
                this.Close();
                regaddfunc.Show();
                regaddfunc.StartHide();
                
            }
        }
    }
}
