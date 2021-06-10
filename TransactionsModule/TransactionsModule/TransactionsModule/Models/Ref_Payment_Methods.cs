using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsModule.Models
{
    public class Ref_Payment_Methods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_Method_Code { get; set; }

        public Payment_Method_Name Payment_Method_Name { get; set; }
    }
}
