using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class Brand
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string MobileModel { get; set; }
        [Required]
        public string UsedYears { get; set; }

    }
}
