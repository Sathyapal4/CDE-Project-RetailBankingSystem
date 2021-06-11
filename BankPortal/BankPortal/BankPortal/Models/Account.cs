using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankPortal.Models
{
    public enum AccountType { Current, Saving }
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AccountId { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
        public double Balance { get; set; }

        [Required]
        public int CustumerId { get; set; }
    }

}
