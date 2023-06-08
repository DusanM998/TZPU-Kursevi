using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TZPU.Services.ShoppingCardAPI.Models.Dtos;

namespace TZPU.Services.ShoppingCardAPI.Models
{
    public class CardDetails
    {
        [Key]
        public int CardDetailsId { get; set; }
        public int CardHeaderId { get; set; }
        [ForeignKey("CardHeaderId")]
        public CardHeader CardHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
    }
}
