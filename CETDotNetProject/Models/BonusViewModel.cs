using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CETDotNetProject.Models
{
    public class BonusViewModel
    {
        [Required(ErrorMessage = "Çalışan alanı zorunludur.")]
        [Display(Name = "Çalışan")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Miktar alanı zorunludur.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Miktar")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Prim Tarihi alanı zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Prim Tarihi")]
        public DateTime BonusDate { get; set; }

        public SelectList Employees { get; set; }
    }
} 