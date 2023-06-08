namespace TZPU.Services.ShoppingCardAPI.Models.Dtos
{
    public class CardDto
    {
        public CardHeaderDto CardHeader { get; set; }
        public IEnumerable<CardDetailsDto>? CardDetails { get; set; }
    }
}
