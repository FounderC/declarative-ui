using System;
using System.Data;
using System.Windows;
using System.Xml.Linq;

namespace Лаб4
{
    public partial class MainWindow : Window
    {
        private AdoAssistant ado = new AdoAssistant();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = ado.TableLoad();
            listBooks.DataContext = dt.DefaultView;

            if (listBooks.Items.Count > 0)
            {
                listBooks.SelectedIndex = 0;
                listBooks.Focus();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (listBooks.SelectedIndex != -1)
            {
                listBooks.SelectedIndex = -1;
                txtISBN.Clear();
                txtName.Clear();
                txtAuthors.Clear();
                txtPublisher.Clear();
                txtYear.Clear();
                MessageBox.Show("Записи очищено. Будь ласка, введіть дані для нового запису та натисніть Create ще раз.");
                return;
            }

            string isbn = txtISBN.Text.Trim();
            string name = txtName.Text.Trim();
            string authors = txtAuthors.Text.Trim();
            string publisher = txtPublisher.Text.Trim();
            int year;

            if (string.IsNullOrEmpty(isbn))
            {
                MessageBox.Show("Поле ISBN не може бути порожнім!");
                return;
            }

            if (!int.TryParse(txtYear.Text.Trim(), out year))
            {
                MessageBox.Show("Некоректний рік видання! Введіть числове значення.");
                return;
            }

            int rowsAffected = ado.InsertRecord(isbn, name, authors, publisher, year);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Новий запис успішно додано!");
            }
            else
            {
                MessageBox.Show("Не вдалося додати запис.");
            }

            listBooks.DataContext = ado.TableLoad().DefaultView;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string isbn = txtISBN.Text;
            string name = txtName.Text;
            string authors = txtAuthors.Text;
            string publisher = txtPublisher.Text;
            int year;
            if (!int.TryParse(txtYear.Text, out year))
            {
                MessageBox.Show("Некоректний рік видання!");
                return;
            }

            int rows = ado.UpdateRecord(isbn, name, authors, publisher, year);
            MessageBox.Show("Оновлено записів: " + rows);
            listBooks.DataContext = ado.TableLoad().DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string isbn = txtISBN.Text;
            int rows = ado.DeleteRecord(isbn);
            MessageBox.Show("Видалено записів: " + rows);
            listBooks.DataContext = ado.TableLoad().DefaultView;
        }
    }
}