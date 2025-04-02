using Lab3;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        private double? _firstValue = null;
        private string _operation = null;
        private bool _isNewEntry = true;

        public MainWindow()
        {
            InitializeComponent();

            // Створюємо CommandBinding
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

            // Крапка
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Dot, Execute_Dot, CanExecute_Always));

            // Операції
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Add, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Subtract, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Multiply, Execute_Operation, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Divide, Execute_Operation, CanExecute_Always));

            CommandBindings.Add(new CommandBinding(CalculatorCommands.Clear, Execute_Clear, CanExecute_Always));
            CommandBindings.Add(new CommandBinding(CalculatorCommands.Calculate, Execute_Calculate, CanExecute_Always));

            // Початковий текст
            txtDisplay.Text = "0";
        }

        // ---- Цифри ----
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
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = num;
                else
                    txtDisplay.Text += num;
            }
        }

        private void Execute_Dot(object sender, ExecutedRoutedEventArgs e)
        {
            if (_isNewEntry)
            {
                txtDisplay.Text = "0.";
                _isNewEntry = false;
            }
            else
            {
                if (!txtDisplay.Text.Contains("."))
                    txtDisplay.Text += ".";
            }
        }

        // ---- Операції (+, -, ×, ÷) ----
        private void Execute_Operation(object sender, ExecutedRoutedEventArgs e)
        {
            double value;
            if (double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                if (_firstValue.HasValue && _operation != null && !_isNewEntry)
                {
                    _firstValue = CalculateResult(_firstValue.Value, value, _operation);
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

        // ---- Кнопка "=" ----
        private void Execute_Calculate(object sender, ExecutedRoutedEventArgs e)
        {
            if (_firstValue.HasValue && _operation != null && !_isNewEntry)
            {
                double secondValue;
                if (double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out secondValue))
                {
                    double? result = CalculateResult(_firstValue.Value, secondValue, _operation);
                    if (result.HasValue)
                    {
                        txtDisplay.Text = result.Value.ToString(CultureInfo.InvariantCulture);
                        _firstValue = result.Value;
                    }
                }
            }
            _operation = null;
            _isNewEntry = true;
        }

        // ---- "C" (Clear) ----
        private void Execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            _firstValue = null;
            _operation = null;
            _isNewEntry = true;
        }

        private double? CalculateResult(double first, double second, string op)
        {
            switch (op)
            {
                case "+": return first + second;
                case "-": return first - second;
                case "*": return first * second;
                case "/":
                    if (second == 0)
                    {
                        MessageBox.Show("Ділення на нуль неможливе!");
                        return null;
                    }
                    return first / second;
                default: return second;
            }
        }

        private void CanExecute_Always(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
