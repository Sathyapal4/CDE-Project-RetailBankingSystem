using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsModule.Models
{
    public class Ref_Transaction_Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Trans_Status_Code { get; set; }
        public Trans_Status_Description Trans_Status_Description { get; set; }
    }
    
}
