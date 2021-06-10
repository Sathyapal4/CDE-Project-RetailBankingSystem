using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagementModule.Models
{
    public class TransactionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }
        [Range(0, double.MaxValue)]
        public double SourceBalance { get; set; }
        [Range(0, double.MaxValue)]
        public double DestinationBalance { get; set; }
        [Range(0, double.MaxValue)]
        public double currentBalance { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
