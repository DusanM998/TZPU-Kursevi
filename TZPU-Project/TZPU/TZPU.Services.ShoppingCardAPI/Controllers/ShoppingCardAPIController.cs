using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using TZPU.Services.ShoppingCardAPI.DbContexts;
using TZPU.Services.ShoppingCardAPI.Models;
using TZPU.Services.ShoppingCardAPI.Models.Dtos;
using TZPU.Services.ShoppingCardAPI.Service.IService;

namespace TZPU.Services.ShoppingCardAPI.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class ShoppingCardAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContexts _db;
        private IProductService _productService;
        private ICouponService _couponService;

        public ShoppingCardAPIController(AppDbContexts db, IMapper mapper, 
            IProductService productService, ICouponService couponService)
        {
            _db = db;
            _mapper = mapper;
            _productService = productService;
            this._response = new ResponseDto();
            _couponService = couponService;

        }

        [HttpGet("GetCard/{userId}")]
        public async Task<ResponseDto> GetCard(string userId)
        {
            try
            {
                CardDto card = new()
                {
                    CardHeader = _mapper.Map<CardHeaderDto>(_db.CardHeaders.First(u => u.UserId == userId))
                };
                card.CardDetails = _mapper.Map<IEnumerable<CardDetailsDto>>(_db.CardDetails
                    .Where(u => u.CardHeaderId == card.CardHeader.CardHeaderId));

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

                foreach(var item in card.CardDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    card.CardHeader.CardTotal += item.Product.Price;
                }

                //apply coupon
                if(!string.IsNullOrEmpty(card.CardHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(card.CardHeader.CouponCode);
                    if(coupon != null && card.CardHeader.CardTotal > coupon.MinAmount)
                    {
                        card.CardHeader.CardTotal -= coupon.DiscountAmount;
                        card.CardHeader.Discount = coupon.DiscountAmount;
                    }
                }

                _response.Result = card;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CardDto cardDto)
        {
            try
            {
                var cardFromDb = await _db.CardHeaders.FirstAsync(u => u.UserId == cardDto.CardHeader.UserId);
                cardFromDb.CouponCode = cardDto.CardHeader.CouponCode;
                _db.CardHeaders.Update(cardFromDb);

                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CardDto cardDto)
        {
            try
            {
                var cardFromDb = await _db.CardHeaders.FirstAsync(u => u.UserId == cardDto.CardHeader.UserId);
                cardFromDb.CouponCode = "";
                _db.CardHeaders.Update(cardFromDb);

                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("CardUpsert")]
        public async Task<ResponseDto> CardUpsert(CardDto cardDto)
        {
            try
            {
                var cardHeaderFromDb = await _db.CardHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cardDto.CardHeader.UserId);
                if(cardHeaderFromDb == null)
                {
                    //Kreiranje header-a i details-a
                    CardHeader cardHeader = _mapper.Map<CardHeader>(cardDto.CardHeader);
                    _db.CardHeaders.Add(cardHeader);
                    await _db.SaveChangesAsync();

                    cardDto.CardDetails.First().CardHeaderId = cardHeader.CardHeaderId;
                    _db.CardDetails.Add(_mapper.Map<CardDetails>(cardDto.CardDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // Ako header nije null
                    // Proveravamo detalje za isti proizvod
                    var cardDetailsFromDb = await _db.CardDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cardDto.CardDetails.First().ProductId && 
                        u.CardHeaderId == cardHeaderFromDb.CardHeaderId);

                    if(cardDetailsFromDb == null)
                    {
                        //create cardDetails
                        cardDto.CardDetails.First().CardHeaderId = cardHeaderFromDb.CardHeaderId;
                        _db.CardDetails.Add(_mapper.Map<CardDetails>(cardDto.CardDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        cardDto.CardDetails.First().CardHeaderId = cardDetailsFromDb.CardHeaderId;
                        cardDto.CardDetails.First().CardDetailsId = cardDetailsFromDb.CardDetailsId;
                        _db.CardDetails.Update(_mapper.Map<CardDetails>(cardDto.CardDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cardDto;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("RemoveCard")]
        public async Task<ResponseDto> RemoveCard([FromBody] int cardDetailsId)
        {
            try
            {
                CardDetails cardDetails = _db.CardDetails.First(u => u.CardDetailsId == cardDetailsId);

                int totalCountOfCardItem = _db.CardDetails.Where(
                    u => u.CardHeaderId == cardDetails.CardHeaderId).Count();

                _db.CardDetails.Remove(cardDetails);

                if(totalCountOfCardItem == 1)
                {
                    var cardHeaderToRemove = await _db.CardHeaders
                        .FirstOrDefaultAsync(u => u.CardHeaderId == cardDetails.CardHeaderId);

                    _db.CardHeaders.Remove(cardHeaderToRemove);
                }

                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
