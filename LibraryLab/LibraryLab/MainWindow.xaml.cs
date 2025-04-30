using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace LibraryLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Спробуємо відкрити з’єднання з БД
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("З'єднання з базою даних встановлено!");

                    // Тут можна виконати запити, зчитати таблиці, заповнити ListView чи DataGrid тощо.
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка з'єднання: {ex.Message}");
                }
            }
        }
    }
}
