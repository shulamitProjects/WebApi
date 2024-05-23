using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }
        //כניסה
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDTO userLogin)
        {
            var user = await _loginService.Authenticate(userLogin);
            if (user != null)
            {
                object token = _loginService.Generate(user);
                var jsonToken = JsonConvert.SerializeObject(new { token });
                return Ok(new { jsonToken });
                //return Ok(token)
            }
            return NotFound("User not found");
        }
        //רישום
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(string firstName, string lastName, string password, string email, string phoneNumber)
        {
            if(password == null || email == null || phoneNumber == null || firstName == null || lastName == null)
            {
                return NotFound("all fields are requirde");
            }
            else
            {
                var user = new User { Password = password, FirstName = firstName, LastName = lastName, PhonNumber = phoneNumber, Email = email };
                var registered = await _loginService.AddUser(user);
                return Ok(registered);
            }
        }
    }
}
