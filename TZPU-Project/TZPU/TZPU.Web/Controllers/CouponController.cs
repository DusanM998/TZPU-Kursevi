using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TZPU.Web.Models;
using TZPU.Web.Service.IService;

namespace TZPU.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Kupon uspešno kreiran!";
                    return RedirectToAction(nameof(CouponIndex));
                }
				else
				{
					TempData["error"] = response?.Message;
				}
			}
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
			ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

			if (response != null && response.IsSuccess)
			{
				CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Kupon uspešno obrisan!";
                return RedirectToAction(nameof(CouponIndex));
            }
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(couponDto);
        }

        public async Task<IActionResult> CouponUpdate(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponUpdate(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.UpdateCouponAsync(couponDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Kupon uspešno ažuriran!";
                return RedirectToAction(nameof(CouponIndex));
            }
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(couponDto);
        }
    }
}
