using System.Windows.Input;

namespace Lab2._4
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Clear =
            new RoutedUICommand("Clear", "Clear", typeof(CustomCommands));
    }
}
