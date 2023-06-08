using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TZPU.Services.ShoppingCardAPI.Models
{
    public class CardHeader
    {
        [Key]
        public int CardHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        public double CardTotal { get; set; }
    }
}
