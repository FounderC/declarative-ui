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

            // Прив'язка команди Save
            CommandBinding saveBinding = new CommandBinding(
                ApplicationCommands.Save,
                Execute_Save,
                CanExecute_Save);
            this.CommandBindings.Add(saveBinding);

            // Прив'язка команди Open
            CommandBinding openBinding = new CommandBinding(
                ApplicationCommands.Open,
                Execute_Open,
                CanExecute_Open);
            this.CommandBindings.Add(openBinding);

            // Прив'язка власної команди Clear
            CommandBinding clearBinding = new CommandBinding(
                CustomCommands.Clear,
                Execute_Clear,
                CanExecute_Clear);
            this.CommandBindings.Add(clearBinding);
        }

        // Обробник CanExecute для Save
        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(txtDocument.Text);
        }

        // Обробник Execute для Save
        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("D:\\myFile.txt", txtDocument.Text);
                MessageBox.Show("Файл збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        // Обробник CanExecute для Open
        private void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Обробник Execute для Open
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

        // Обробник CanExecute для Clear
        private void CanExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(txtDocument.Text);
        }

        // Обробник Execute для Clear
        private void Execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            txtDocument.Clear();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Можлива додаткова логіка
        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Можлива додаткова логіка
        }
    }
}
