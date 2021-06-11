using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBankingClient.Models.Account
{

    public class Statement
    {
        [Display(Name = "Statement ID")]
        public int Id { get; set; }
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Narration")]
        public string Narration { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        [Display(Name = "Value Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ValueDate { get; set; }
        [Display(Name = "Withdrawal")]
        public double Withdrawal { get; set; }
        [Display(Name = "Deposit")]
        public double Deposit { get; set; }
        [Display(Name = "Closing Balance")]
        public double ClosingBalance { get; set; }



        
    }
}
