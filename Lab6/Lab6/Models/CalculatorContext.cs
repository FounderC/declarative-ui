using System.Data.Entity;

namespace Lab6.Models
{
    /// <summary>
    /// EF Code-First контекст для таблиці CalculationHistory
    /// </summary>
    public class CalculatorContext : DbContext
    {
        public CalculatorContext()
            : base("CalculatorConnection")   // ім’я з’єднання з App.config
        {
        }

        public DbSet<CalculationHistory> History { get; set; }
    }
}
