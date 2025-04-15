using System.Data;
using System.Windows;
using System.Windows.Controls;
using Лаб4;

namespace Лаб4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();  // Викликаємо метод завантаження даних при запуску вікна
        }

        private void LoadData()
        {
            // Створення екземпляру класу AdoAssistant
            AdoAssistant ado = new AdoAssistant();

            // Отримання DataTable з даними
            DataTable dt = ado.TableLoad();

            // Прив'язка даних до DataGrid. Використовується DefaultView, щоб DataGrid міг коректно їх відобразити
            dataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
