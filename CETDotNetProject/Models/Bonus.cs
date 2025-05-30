using System;
using System.ComponentModel.DataAnnotations;

namespace CETDotNetProject.Models
{
    public class Bonus
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Prim tutarı zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Prim tutarı sıfırdan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Prim Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BonusDate { get; set; } = DateTime.Now;
    }
} 