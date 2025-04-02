using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Lab2._4
{
    public partial class _4 : Window
    {
        public _4()
        {
            InitializeComponent();

            // Save
            CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save,
                                                            Execute_Save,
                                                            CanExecute_Save);
            this.CommandBindings.Add(saveBinding);

            // Open
            CommandBinding openBinding = new CommandBinding(ApplicationCommands.Open,
                                                            Execute_Open,
                                                            CanExecute_Open);
            this.CommandBindings.Add(openBinding);

            // Clear (власна команда)
            CommandBinding clearBinding = new CommandBinding(CustomCommands.Clear,
                                                             Execute_Clear,
                                                             CanExecute_Clear);
            this.CommandBindings.Add(clearBinding);
        }

        // Save
        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(txtDocument.Text);
        }
        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("D:\\myFile.txt", txtDocument.Text);
                MessageBox.Show("Файл збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        // Open
        private void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            if (File.Exists("D:\\myFile.txt"))
            {
                txtDocument.Text = File.ReadAllText("D:\\myFile.txt");
                MessageBox.Show("Файл відкрито!");
            }
            else
            {
                MessageBox.Show("Файл не знайдено.");
            }
        }

        // Clear
        private void CanExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(txtDocument.Text);
        }
        private void Execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            txtDocument.Clear();
        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Додаткова логіка при зміні тексту (якщо треба)
        }
    }
}
