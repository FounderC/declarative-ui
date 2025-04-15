using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Лаб4
{
    public class AdoAssistant
    {
        // Отримання рядка підключення з App.config (переконайтеся, що ім'я відповідає)
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;

        // Змінна для зберігання даних (DataTable)
        private DataTable dt = null;

        // Метод, що завантажує дані з БД у DataTable
        public DataTable TableLoad()
        {
            if (dt != null)
                return dt;

            dt = new DataTable();

            // Використовуємо using для автоматичного закриття підключення
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Створення команди
                SqlCommand command = connection.CreateCommand();
                // Наприклад, вибірка даних із таблиці Table_Address
                command.CommandText = "SELECT ID, (Surname + ' ' + Name) AS FullName, Address, Place, PostalCode FROM Table_Address";

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    // Відкриття підключення та завантаження даних
                    connection.Open();
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка підключення до БД: " + ex.Message);
                }
            }

            return dt;
        }
    }
}
