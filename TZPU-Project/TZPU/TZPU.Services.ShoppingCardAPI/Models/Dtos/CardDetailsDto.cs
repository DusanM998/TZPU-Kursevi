namespace TZPU.Services.ShoppingCardAPI.Models.Dtos
{
    public class CardDetailsDto
    {
        public int CardDetailsId { get; set; }
        public int CardHeaderId { get; set; }
        public CardHeaderDto? CardHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
    }
}
