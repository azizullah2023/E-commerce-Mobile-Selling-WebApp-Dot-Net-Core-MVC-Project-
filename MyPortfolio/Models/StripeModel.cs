using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class StripeModel
    {
        [Key]
        public int Id { get; set; }
        public string? SessionI { get; set; }
        public string? PaymentIntentId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
