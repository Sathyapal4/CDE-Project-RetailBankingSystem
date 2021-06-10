using System.ComponentModel.DataAnnotations;

namespace AccountManagementModule.Models
{
    public class InputAmountFromUser
    {
        [Required]
        public int AccountId { get; set; }

        [Range(0, double.MaxValue)]
        [Required]
        public double Amount { get; set; }

        public string Narration { get; set; }
    }
}
