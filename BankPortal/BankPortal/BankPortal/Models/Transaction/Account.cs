using System.ComponentModel.DataAnnotations;

namespace RetailBankingClient.Models.Transaction
{
    public class Account
    {
        [Display(Name = "Account ID")]
        [Range(0, int.MaxValue)]
        [Required]
        public int AccountId { get; set; }
        [Display(Name = "Amount")]
        [Range(0, double.MaxValue)]
        [Required]
        public double Amount { get; set; }
        [Display(Name = "Narration")]
        public string Narration { get; set; }
    }
    public class Transfer
    {
        [Range(0, int.MaxValue)]
        [Display(Name = "Select Your Account Id")]
        [Required]
        public int Source_AccountId { get; set; }
        [Range(0, int.MaxValue)]
        [Display(Name = "Enter Receiver Account Id")]
        [Required]
        public int Target_AccountId { get; set; }
        [Range(0, double.MaxValue)]
        [Required]
        public double Amount { get; set; }
    }
    public class Ref_Transaction_Status
    {
        [Display(Name = "Transaction Code")]
        public int Trans_Status_Code { get; set; }
        [Display(Name = "Transaction Status")]
        public Trans_Status_Description Trans_Status_Description { get; set; }
    }
    public enum Trans_Status_Description
    {
        Cancelled, Completed, Disputed
    }
}
