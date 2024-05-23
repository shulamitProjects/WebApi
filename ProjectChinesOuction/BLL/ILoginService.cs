using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.BLL
{
    public interface ILoginService
    {
        public Task<List<User>> AddUser(User user);
        public string Generate(User user);//רישום
        public Task<User> Authenticate(UserLoginDTO userLogin);//כניסה
    }
}
