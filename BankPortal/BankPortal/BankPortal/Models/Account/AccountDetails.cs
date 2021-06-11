using System.ComponentModel.DataAnnotations;

namespace RetailBankingClient.Models.Account
{
    public class AccountDetails
    {
        [Display(Name = "Account ID")]
        [Required]
        public int AccountId { get; set; }
        [Display(Name = "Account Type")]
        [Required]
        public AccountType AccountType { get; set; }
        [Display(Name = "Balance")]
        [Required]
        public double Balance { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
    }
}
