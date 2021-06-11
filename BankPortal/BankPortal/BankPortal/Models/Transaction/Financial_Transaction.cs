using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBankingClient.Models.Transaction
{
    public class Financial_Transaction
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Transaction Id")]
        public int Transaction_ID { get; set; }
        [Display(Name = "Account Id")]
        public int Account_ID { get; set; }
        public int Counterparty_ID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date Of Transaction")]
        public DateTime Date_of_Transaction { get; set; }
        [Display(Name = "Transaction Amount")]
        public double Amount_of_Transaction { get; set; }
        public string Other_Details { get; set; }
        public int Payment_Method_Code { get; set; }
        public int Trans_Type_Code { get; set; }
        [Display(Name = "Transaction Status")]
        public int Trans_Status_Code { get; set; }
        public int Service_ID { get; set; }
        [ForeignKey("Payment_Method_Code")]
        public Ref_Payment_Methods Ref_Payment_Methods { get; set; }
        [ForeignKey("Service_ID")]
        public Services Services { get; set; }
        [ForeignKey("Trans_Status_Code")]
        public Ref_Transaction_Status Ref_Transaction_Status { get; set; }
        [ForeignKey("Trans_Type_Code")]
        public Ref_Transaction_Types Ref_Transaction_Types { get; set; }
        [ForeignKey("Counterparty_ID")]
        public Counterparties Counterparties { get; set; }


    }
}
