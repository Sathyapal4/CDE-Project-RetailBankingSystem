using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsModule.Models
{
    public class Ref_Transaction_Types
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Trans_Type_Code { get; set; }
        public Trans_Type_Description Trans_Type_Description { get; set; }
    }
}
