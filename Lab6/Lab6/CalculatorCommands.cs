using System.Windows.Input;

namespace Lab6
{
    public static class CalculatorCommands
    {
        // Цифри 0–9
        public static readonly RoutedUICommand Num0 =
            new RoutedUICommand("Num0", "Num0", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num1 =
            new RoutedUICommand("Num1", "Num1", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num2 =
            new RoutedUICommand("Num2", "Num2", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num3 =
            new RoutedUICommand("Num3", "Num3", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num4 =
            new RoutedUICommand("Num4", "Num4", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num5 =
            new RoutedUICommand("Num5", "Num5", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num6 =
            new RoutedUICommand("Num6", "Num6", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num7 =
            new RoutedUICommand("Num7", "Num7", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num8 =
            new RoutedUICommand("Num8", "Num8", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Num9 =
            new RoutedUICommand("Num9", "Num9", typeof(CalculatorCommands));

        // Крапка (.)
        public static readonly RoutedUICommand Dot =
            new RoutedUICommand("Dot", "Dot", typeof(CalculatorCommands));

        // Операції
        public static readonly RoutedUICommand Add =
            new RoutedUICommand("Add", "Add", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Subtract =
            new RoutedUICommand("Subtract", "Subtract", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Multiply =
            new RoutedUICommand("Multiply", "Multiply", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Divide =
            new RoutedUICommand("Divide", "Divide", typeof(CalculatorCommands));

        public static readonly RoutedUICommand Clear =
            new RoutedUICommand("Clear", "Clear", typeof(CalculatorCommands));
        public static readonly RoutedUICommand Calculate =
            new RoutedUICommand("Calculate", "Calculate", typeof(CalculatorCommands));
    }
}