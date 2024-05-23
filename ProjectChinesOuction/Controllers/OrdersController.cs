using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectChinesOuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _ordersService;
        private readonly IPresentService _presentService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService ordersService, IMapper mapper, IPresentService presentService)
        {
            _ordersService = ordersService ?? throw new ArgumentNullException(nameof(_ordersService));
            this._mapper = mapper;
            _presentService = presentService ?? throw new ArgumentNullException();
        }
        //קבל את הרכישות עבור כל מתנה
        [Authorize(Roles="admin")]
        [HttpGet]
        [Route("GetThePurchasesForEachPresent")]
        public async Task<ActionResult<List<OrderItemDTO>>> GetThePurchasesForEachPresent(int presentId)
        {
            try
            {
                var PoForPresent = await _ordersService.GetThePurchasesForEachPresent(presentId);
                var _PoForPresent = _mapper.Map<List<OrderItem>>(PoForPresent);
                return Ok(_PoForPresent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetThePurchasesForEachPresent function");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("GetTheAllPurchases")]
        public async Task<ActionResult<List<User>>> GetTheAllPurchases()
        {
            try
            {
                var PoForPresent = await _ordersService.GetTheAllPurchases();
                //var _PoForPresent = _mapper.Map<List<OrderItem>>(PoForPresent);
                return Ok(PoForPresent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetThePurchasesForEachPresent function");
            }
        }
        //מיון רכישות לפי המתנה היקרה ביותר
        // GET: api/<OrdersController>
        [HttpGet("GetPresentOrderedByPrice")]
        public async Task<ActionResult<List<Gift>>> GetPresentOrderedByPrice()
        {
            try
            {
                return await _ordersService.GetPresentOrderedByPrice();
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
        //מיון רכישות לפני המתנה הנרכשת ביותר
        [HttpGet("GetGiftsOrderedByCount")]
        public async Task<ActionResult<List<Gift>>> GetGiftsOrderedByCount()
        {
            try
            { 
                return await _ordersService.GetGiftsOrderedByCount();
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
         //לקבל רשימת הזמנות
        // GET: api/<PresentsOrderController>
        [HttpGet]
        [Route("GetPresentsOrder")]
        public async Task<ActionResult<List<OrderItemDTO>>> GetPresentsOrder(int userId)
        {
            try
            {
                var allPo = await _ordersService.GetGiftsOrderedByUser(userId);
                var _AllPo = _mapper.Map<List<OrderItem>>(allPo);
                return Ok(_AllPo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetPresentsOrder function");
            }
        }
    }
}
