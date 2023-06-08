﻿namespace TZPU.Services.ShoppingCardAPI.Models.Dtos
{
    public class CardHeaderDto
    {
        public int CardHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double CardTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
