using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CETDotNetProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Departman alanı zorunludur.")]
        [Display(Name = "Departman")]
        public string Department { get; set; }

        [Display(Name = "İşe Başlama Tarihi")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Maaş")]
        public decimal Salary { get; set; }

        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; } = true;
    }
} 