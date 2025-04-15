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
        }

        // Подія, що спрацьовує після завантаження вікна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Створюємо екземпляр класу доступу до БД
            AdoAssistant ado = new AdoAssistant();

            // Викликаємо метод, який повертає DataTable з даними
            DataTable booksTable = ado.TableLoad();

            // Прив'язуємо дані до ListBox - тепер кожен рядок відображається у списку
            // (Поле списку = ISBN, що ми вказали в XAML через DisplayMemberPath)
            listBooks.DataContext = booksTable.DefaultView;

            // Якщо потрібно, можна відразу вибрати перший елемент списку
            listBooks.SelectedIndex = 0;
            listBooks.Focus();
        }
    }
}
