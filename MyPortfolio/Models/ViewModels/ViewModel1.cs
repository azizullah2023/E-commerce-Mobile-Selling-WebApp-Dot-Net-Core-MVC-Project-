using MyPortfolio.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Models.ViewModels
{
    public class ViewModel1
    {
        public IEnumerable<FormMobileSellIt> MobileSellIt { get; set; }
        public IEnumerable<Comment> Comment { get; set; }
        [NotMapped]
        public IEnumerable<Likes> rowsnumbr  { get; set; }
        [NotMapped]
        public string? CountLikes { get; set; }
        
       
    }
}
