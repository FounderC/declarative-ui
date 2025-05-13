using System;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    /// <summary>
    /// Запис у історії обчислень
    /// </summary>
    public class CalculationHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Expression { get; set; }

        [Required]
        [MaxLength(50)]
        public string Result { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
