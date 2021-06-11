using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankPortal.Models
{

    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ValidAge(ErrorMessage = "Age Must be over 18")]
        public DateTime DOB { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        [Display(Name = "Pan Number")]
        public string PAN { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8), MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [MinLength(8), MaxLength(16)]
        [Compare("Password")]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; } = DateTime.Now;

    }

    public class ValidAge : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dob = DateTime.Parse(value.ToString());
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age--;
            return age >= 18;
        }
    }
}


