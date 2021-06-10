using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagementModule.Models
{

    public class Statement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Narration { get; set; }

        [Required]
        public string RefNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ValueDate { get; set; }
        public double Withdrawal { get; set; }
        public double Deposit { get; set; }

        [Required]
        public double ClosingBalance { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
