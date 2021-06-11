using System;
using System.ComponentModel.DataAnnotations;

namespace RetailBankingClient.Models.Account
{
    public class GetStatement
    {
        [Required]
        [Display(Name = "Account ID")]
        public int Id { get; set; }


        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fromDate { get; set; }
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime toDate { get; set; }
    }
}
