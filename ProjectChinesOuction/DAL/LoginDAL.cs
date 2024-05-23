using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.DAL
{
    public class LoginDAL : ILoginDAL
    {
        private readonly OctionContext _oactionContext;
        public LoginDAL(OctionContext oactionContext)
        {
            _oactionContext = oactionContext ?? throw new ArgumentNullException(nameof(_oactionContext));
        }
        //רישום
        public async Task<List<User>> AddUserDal(User user)
        {
            await _oactionContext.User.AddAsync(user);
            await _oactionContext.SaveChangesAsync();
            var u=_oactionContext.User.ToListAsync();
            return await u;
        }
        //כניסה
        public async Task<User> AuthenticateDal(UserLoginDTO userLogin)
        {
            var currentUser=_oactionContext.User.FirstOrDefault(o=>o.Email.ToLower()==
            userLogin.Email.ToLower()&&o.Password==userLogin.Password);
            if (currentUser!=null)
            {
                return currentUser;
            }
            return null;
        }
    }
    //רישום
    //כניסה
}
