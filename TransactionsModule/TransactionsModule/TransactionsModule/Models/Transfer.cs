using System.ComponentModel.DataAnnotations;

namespace TransactionsModule.Models
{
    public class Transfer
    {
        [Required]
        public int SourceAccountId { get; set; }
        [Required]
        public int DestinationAccountId { get; set; }
        [Range(0, double.MaxValue)]
        [Required]
        public double Amount { get; set; }
    }
}
