using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagementModule.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
        [Range(0, double.MaxValue)]
        public double Balance { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
