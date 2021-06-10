using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsModule.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }

        public string Narration { get; set; }
    }
}
