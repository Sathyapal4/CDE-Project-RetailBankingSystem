using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBankingClient.Models.Transaction
{
    public class TransactionStatus
    {
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Source Balance")]
        public double SourceBalance { get; set; }
        [Display(Name = "Destination Balance")]
        public double DestinationBalance { get; set; }
        [Display(Name = "Current Balance")]
        public double currentBalance { get; set; }
    }
}
