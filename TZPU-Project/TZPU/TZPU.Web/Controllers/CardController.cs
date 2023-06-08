using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TZPU.Web.Models;
using TZPU.Web.Service.IService;
using TZPU.Web.Utility;

namespace TZPU.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IOrderService _orderService;
        public CardController(ICardService cardService, IOrderService orderService)
        {
            _cardService = cardService;
            _orderService = orderService;

        }

        [Authorize]
        public async Task<IActionResult> CardIndex()
        {
            return View(await LoadCardDtoBasedOnLoggedUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCardDtoBasedOnLoggedUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CardDto cardDto)
        {
            var card = await LoadCardDtoBasedOnLoggedUser();

            card.CardHeader.Phone = cardDto.CardHeader.Phone;
            card.CardHeader.Email = cardDto.CardHeader.Email;
            card.CardHeader.Name = cardDto.CardHeader.Name;

            var response = await _orderService.CreateOrder(card);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));

            if(response != null && response.IsSuccess)
            {
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "card/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "card/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));

                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);

                return new StatusCodeResult(303);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto? response = await _orderService.ValidateStripeSession(orderId);

            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if(orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
                return RedirectToAction(nameof(CardIndex));
            }
            return View(orderId);
        }


        private async Task<CardDto> LoadCardDtoBasedOnLoggedUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cardService.GetCardByUserIdAsync(userId);

            if(response != null && response.IsSuccess)
            {
                CardDto cardDto = JsonConvert.DeserializeObject<CardDto>(Convert.ToString(response.Result));
                return cardDto;
            }
            return new CardDto();
        }

        public async Task<IActionResult> Remove(int cardDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cardService.RemoveFromCardAsync(cardDetailsId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Stanje korpe ažurirano!";
                return RedirectToAction(nameof(CardIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CardDto cardDto)
        {
            ResponseDto? response = await _cardService.ApplyCouponAsync(cardDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Stanje korpe ažurirano!";
                return RedirectToAction(nameof(CardIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CardDto cardDto)
        {
            cardDto.CardHeader.CouponCode = "";
            ResponseDto? response = await _cardService.ApplyCouponAsync(cardDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Stanje korpe ažurirano!";
                return RedirectToAction(nameof(CardIndex));
            }
            return View();
        }
    }
}
