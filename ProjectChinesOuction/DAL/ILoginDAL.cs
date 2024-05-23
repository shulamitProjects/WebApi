using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.DAL
{
    public interface ILoginDAL
    {
        public Task<List<User>> AddUserDal(User user);//רישום
        //public string Generate(User user);
        public Task<User> AuthenticateDal(UserLoginDTO userLogin);//כניסה
    }
}
