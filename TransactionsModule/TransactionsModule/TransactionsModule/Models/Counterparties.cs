using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsModule.Models
{
    public class Counterparties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Counterparty_ID { get; set; }

        public string Other_Details { get; set; }
    }
}
