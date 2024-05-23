using Microsoft.IdentityModel.Tokens;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectChinesOuction.BLL
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDAL _userDal;
        private readonly IConfiguration _config;
        public LoginService(ILoginDAL userDal, IConfiguration config)
        {
            _userDal = userDal;
            _config = config;
        }
        public async Task<List<User>> AddUser(User user)
        {
            return await _userDal.AddUserDal(user);
        }

        public async Task<User> Authenticate(UserLoginDTO userLogin)
        {
            return await _userDal.AuthenticateDal(userLogin);
        }

        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim("email", username),
                 new Claim(ClaimTypes.Role,user.Role.ToString()),
                 new Claim("userId",user.UserId.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer ="http://localhost:7214/",
                Audience ="http://localhost:7214/",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };




            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
