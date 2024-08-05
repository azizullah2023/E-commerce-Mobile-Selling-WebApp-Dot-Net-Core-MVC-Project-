using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int MobileSellingID { get; set; }

        [ForeignKey("MobileSellingID")]
        public FormMobileSellIt MobileSellIt { get; set; }
        public string ComntConetent { get; set; }
        public DateTime CraetedAt { get; set; }
        public int MobileSellId { get; set; }
        
        public int?  Like{ get; set; }
        public bool isLiked { get; set; }
    }
}
