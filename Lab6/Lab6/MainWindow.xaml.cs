using Lab6.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        private double? _firstValue = null;
        private string _operation = null;
        private bool _isNewEntry = true;

        private readonly CalculatorContext _context = new CalculatorContext();
        private SqlDataAdapter _adapter;
        private DataTable _table;

        public MainWindow()
        {
            InitializeComponent();
            TestDbConnection();
            InitializeCommands();
            txtDisplay.Text = "0";
            LoadHistory();
        }
        private void TestDbConnection()
        {
            var connStr = ConfigurationManager
                              .ConnectionStrings["CalculatorConnection"]
                              .ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Debug.WriteLine("Підключення до БД успішне.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Помилка підключення до БД: " + ex.Message);
                    MessageBox.Show(
                        "Не вдалося підключитися до бази даних:\n" + ex.Message,
                        "Помилка підключення",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num0, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num1, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num2, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num3, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num4, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num5, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num6, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num7, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num8, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Num9, Execute_Num, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Dot, Execute_Dot, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Add, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Subtract, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Multiply, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Divide, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Clear, Execute_Clear, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Calculate, Execute_Calculate, CanExecute_Always));
        }

        private void LoadHistory()
        {
            var connStr = ConfigurationManager
                              .ConnectionStrings["CalculatorConnection"]
                              .ConnectionString;

            _adapter = new SqlDataAdapter(
                "SELECT * FROM CalculationHistory ORDER BY CreatedAt DESC",
                connStr);
            _table = new DataTable();
            _adapter.Fill(_table);
            dgHistory.ItemsSource = _table.DefaultView;
        }

        private void Execute_Num(object sender, ExecutedRoutedEventArgs e)
        {
            string num = null;
            if (e.Command == CalculatorCommands.Num0) num = "0";
            else if (e.Command == CalculatorCommands.Num1) num = "1";
            else if (e.Command == CalculatorCommands.Num2) num = "2";
            else if (e.Command == CalculatorCommands.Num3) num = "3";
            else if (e.Command == CalculatorCommands.Num4) num = "4";
            else if (e.Command == CalculatorCommands.Num5) num = "5";
            else if (e.Command == CalculatorCommands.Num6) num = "6";
            else if (e.Command == CalculatorCommands.Num7) num = "7";
            else if (e.Command == CalculatorCommands.Num8) num = "8";
            else if (e.Command == CalculatorCommands.Num9) num = "9";

            if (num == null) return;
            if (_isNewEntry)
            {
                txtDisplay.Text = num;
                _isNewEntry = false;
            }
            else
            {
                txtDisplay.Text = txtDisplay.Text == "0" ? num : txtDisplay.Text + num;
            }
        }

        private void Execute_Dot(object sender, ExecutedRoutedEventArgs e)
        {
            if (_isNewEntry)
            {
                txtDisplay.Text = "0.";
                _isNewEntry = false;
            }
            else if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }

        private void Execute_Operation(object sender, ExecutedRoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                if (_firstValue.HasValue && _operation != null && !_isNewEntry)
                {
                    _firstValue = Calculate(_firstValue.Value, value, _operation);
                    txtDisplay.Text = _firstValue.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    _firstValue = value;
                }
            }

            if (e.Command == CalculatorCommands.Add) _operation = "+";
            else if (e.Command == CalculatorCommands.Subtract) _operation = "-";
            else if (e.Command == CalculatorCommands.Multiply) _operation = "*";
            else if (e.Command == CalculatorCommands.Divide) _operation = "/";

            _isNewEntry = true;
        }
        private void Execute_Calculate(object sender, ExecutedRoutedEventArgs e)
        {
            if (_firstValue.HasValue && _operation != null && !_isNewEntry
                && double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var secondValue))
            {
                double originalFirst = _firstValue.Value;
                string op = _operation;

                double result = Calculate(originalFirst, secondValue, op);

                txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);

                var record = new CalculationHistory
                {
                    Expression = $"{originalFirst}{op}{secondValue}",    // тепер "333/3"
                    Result = result.ToString(CultureInfo.InvariantCulture),
                    CreatedAt = DateTime.Now
                };
                _context.History.Add(record);
                _context.SaveChanges();

                var row = _table.NewRow();
                row["Expression"] = record.Expression;
                row["Result"] = record.Result;
                row["CreatedAt"] = record.CreatedAt;
                _table.Rows.Add(row);
                new SqlCommandBuilder(_adapter);
                _adapter.Update(_table);

                _firstValue = result;
            }

            _operation = null;
            _isNewEntry = true;
        }

        private void Execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            _firstValue = null;
            _operation = null;
            _isNewEntry = true;
        }

        private double Calculate(double a, double b, string op)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    if (b == 0)
                    {
                        MessageBox.Show(
                            "Ділення на нуль неможливе!",
                            "Помилка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return a;
                    }
                    return a / b;
                default:
                    return b;
            }
        }

        private void CanExecute_Always(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
    }
}
