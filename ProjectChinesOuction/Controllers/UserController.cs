using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ProjectChinesOuction.DAL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectChinesOuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //-- -----------------------------------------------------------------------------
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        //-------------------------------------------------------------------------------
        public UserController(IUserService user, IMapper gifDto, IConfiguration config)
        {
            _userService = user;
            _mapper = gifDto;
            this._config = config;
        }
        //-------------------------------------------------------------------------------
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                List<User> users = await _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //Get BY ID--------------------------------------------------------------------------
        [HttpGet("GetUsers/{Id}")]
        public async Task<ActionResult<User>> GetById(int Id)
        {

            try
            {
                User user = await _userService.GetUserById(Id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> Add([FromBody] User user)
        {
            //Gift gift = _mapper.Map<GiftDto, Gift>(gift_Dto);
            //var gif = 
            //GiftDto gifDto = _mapper.Map<Gift, GiftDto>(gif);

            try
            {
                User u = await _userService.AddUser(user);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
        //-------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> login([FromBody] UserLoginDTO userLogin)//string userEmail, string userPassword)
        {
            //Gift gift = _mapper.Map<GiftDto, Gift>(gift_Dto);
            //var gif = 
            //GiftDto gifDto = _mapper.Map<Gift, GiftDto>(gif);

            //User user = await _userService.Login(userEmail, userPassword);


            var user = await _userService.Authenticate(userLogin);

            if (user != null)
            {
                string token = Generate(user);
                //var jsonToken = JsonConvert.SerializeObject(new { token });

                return Ok(new { token, user.Role });

            }
            else
            {

                return null;
            }

        }
        private string Generate(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.FirstName),
                new Claim(ClaimTypes.NameIdentifier,user.LastName),
                //new Claim("password",user.UserPassword),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("userId",user.UserId.ToString()),

                };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        //-------------------------------------------------------------------------------

        [HttpDelete("DeleteUser/{ID}")]

        public async Task<ActionResult<bool>> Delete(int ID)
        {
            try
            {
                var flag = await _userService.DeleteUser(ID);
                return Ok(flag);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        //-------------------------------------------------------------------------------

        // POST api/<UserController>
        //[HttpPost("AddGiftToCart")]
        //public async Task<ActionResult<bool>> AddGiftToCart(int GiftID, string userName)
        //{
        //    try
        //    {
        //        var flag = _userService.AddGiftToCart(GiftID, userName);
        //        return Ok(flag);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        [HttpPost("AddGiftToCart")]
        public async Task<bool> Post([FromBody] OrderItemDTO orderItemDto)
        {
            OrderItem o = _mapper.Map<OrderItemDTO, OrderItem>(orderItemDto);
            User user = (User)HttpContext.Request.HttpContext.Items["User"];
            o.OrderItemStatus = 0;
            var orr = await _userService.AddOrderItem(o, user);
            return true;
        }
        //-------------------------------------------------------------------------------

        [HttpPut("purchaseGiftFromCart")]
        public async Task<ActionResult<bool>> purchaseGiftFromCart(int OrderId)
        {
            try
            {
                var flag = _userService.PurchaseGiftFromCart(OrderId);
                return Ok(flag);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //-------------------------------------------------------------------------------
        // DELETE api/<UserController>/5
        [HttpDelete(" RemoveGiftFromCart")]
        public async Task<ActionResult<bool>> removeGiftFromCart([FromBody] OrderItemDTO orderItemDto)
        {
            try
            {
                OrderItem o = _mapper.Map<OrderItemDTO, OrderItem>(orderItemDto);
                User user = (User)HttpContext.Request.HttpContext.Items["User"];
                o.OrderItemStatus = 0;
                var flag = await _userService.RemoveGiftFromCart(o, user);
                return Ok(flag);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]
        // GET: api/<UserController>
        [HttpGet("GetGiftOrderIds")]
        public async Task<List<object>> GetGiftOrderIds()
        {
            try
            {
                return await _userService.GetGiftOrderIds();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------
        [HttpGet("GetGiftOrderIdsById")]
        public async Task<List<object>> GetGiftOrderIdsById()
        {
            try
            {
                User user = (User)HttpContext.Request.HttpContext.Items["User"];

                return await _userService.GetGiftOrderIdsById(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------
        [HttpGet("GetGiftOrderIdsBygiftId")]
        //	צפייה ברכישות כרטיסים עבור כל מתנה.
        public async Task<List<object>> GetGiftOrderIdsBygiftId([FromQuery] int giftId)
        {
            try
            {
                return await _userService.GetGiftOrderIdsBygiftId(giftId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------

        // GET api/<UserController>/5
        [HttpGet("GetGiftsByUserOrderedByPrice/{userLastName}")]
        public async Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userLastName)
        {
            try
            {
                return await _userService.GetGiftsByUserOrderedByPrice(userLastName);
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
        //-------------------------------------------------------------------------------
        // GET api/<UserController>/5
        [HttpGet("GetGiftsByUserOrderedByCategory/{userLastName}")]
        public async Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userLastName)
        {
            try
            {
                return await _userService.GetGiftsByUserOrderedByCategory(userLastName);
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
        //-------------------------------------------------------------------------------
        [HttpGet("winners")]
        public Task<List<object>> GetWinners()
        {
            try
            {
                return _userService.GetWinners();
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
        //private readonly IUserService _userService;
        //private readonly IMapper _mapper;

        //public UserController(IUserService userService, IMapper mapper)
        //{
        //    _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
        //    _mapper = mapper;
        //}
        ////רשימת לקוחות
        //[HttpGet]
        //public async Task<List<User>> GetAsync()
        //{
        //    try
        //    {
        //        return await _userService.GetUserBLL();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        ////לקבל לקוח לפי ID
        //[HttpGet("getById/{id}")]
        //public async Task<ActionResult<User>> getUserById(int id)
        //{
        //    try
        //    {
        //        return await _userService.getUserById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        ////מיון לפי מחיר עבןר כל לקוח
        //[HttpGet("GetGiftsByUserOrderedByPrice/{username}")]
        //public async Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userName)
        //{
        //    try
        //    {
        //        return await _userService.GetGiftsByUserOrderedByPrice(userName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        ////מיןל לפי קטגוריה עבור כל לקוח
        //[HttpGet("GetGiftsByUserOrderedByCategory/{userLastName}")]
        //public async Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userName)
        //{
        //    try
        //    {
        //        return await _userService.GetGiftsByUserOrderedByCategory(userName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        ////הכנסה לסל של בחירת מתנה
        //[HttpPost("AddGiftToCart")]

        //public async Task AddGiftToCart([FromBody] OrderItemDTO item)
        //{
        //    try
        //    {
        //        User user = (User)HttpContext.Request.HttpContext.Items["User"];

        //        await _userService.AddOrder(item,user.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        //[HttpPost("AddOrder")]

        //public async Task<Order> AddOrder([FromBody] OrderDTO order)
        //{
        //    try
        //    {
        //        User user=(User)HttpContext.Request.HttpContext.Items["User"];
        //        order.UserId = user.UserId;
        //       return  await _userService.AddBucket(order);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        //[HttpGet("getBucket")]
        //public async Task<Order> GetBucket()
        //{
        //    User user = (User)HttpContext.Request.HttpContext.Items["User"];
        //    return await _userService.GetBucket(user.UserId);
        //}
        ////רכישת מתנה מהעגלה
        //[HttpPut("purchaseGiftFromCart")]
        //public async Task<string> purchaseGiftFromCart(int OrderId)
        //{
        //    try
        //    {
        //        return await _userService.purchaseGiftFromCart(OrderId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}
        ////מחיקה מהסל
        //[HttpDelete("removeGiftFromCart/{id}")]
        //public async Task<string> removeGiftFromCart(int giftId, int userId)
        //{
        //    try
        //    {
        //        return await _userService.removeGiftFromCart(giftId, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error");
        //    }
        //}

    }
}