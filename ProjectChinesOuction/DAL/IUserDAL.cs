using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.DAL
{
    public interface IUserDAL
    {
        //Task<bool> AddGiftToCartAsync(int giftId, string userName);
        Task<bool> AddOrderItemAsync(OrderItem orderItem, User user);

        //-------------------------------------------------------------------------------

        Task<User> AddUserAsync(User u);
        //-------------------------------------------------------------------------------
        public Task<User> AuthenticateDal(UserLoginDTO userLogin);
        //-------------------------------------------------------------------------------

        Task<bool> DeleteAsync(int ID);
        //-------------------------------------------------------------------------------

        Task<List<object>> GetGiftOrderIdsAsync();
        Task<List<object>> GetGiftOrderIdsByIdAsync(User user);
        Task<List<object>> GetGiftOrderIdsBygiftIdAsync(int giftId);

        //-------------------------------------------------------------------------------

        Task<List<Gift>> GetGiftsByUserOrderedByCategoryAsync(string userName);
        //-------------------------------------------------------------------------------

        Task<List<Gift>> GetGiftsByUserOrderedByPriceAsync(string userName);
        //-------------------------------------------------------------------------------

        Task<User> GetUserByIdAsync(int userId);
        //-------------------------------------------------------------------------------

        Task<List<User>> GetUsersAsync();
        //-------------------------------------------------------------------------------

        Task<List<object>> GetWinnersAsync();
        //-------------------------------------------------------------------------------

        Task<bool> LoginAsync(string userEmail, string userPassword);
        //-------------------------------------------------------------------------------

        Task<bool> PurchaseGiftFromCartAsync(int OrderId);
        //-------------------------------------------------------------------------------

        Task<bool> RemoveGiftFromCartAsync(OrderItem orderItem, User user);
        //-------------------------------------------------------------------------------
        ////לקבל רשימת משתמשים
        //Task<List<User>> GetUserDAL();
        ////סינון לפי ID
        //public Task<User> getUserById(int id);
        ////מסך כניסה
        ////Task<User> login(UserLoginDTO userLogin);
        ////מסך רישום
        //Task<int> CreateUser(User userRegister);
        ////צפיה ברשימת המתנות, מיון לפי מחיר
        //Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userName);
        ////צפיה ברשימת המתנות, מיון לפי קטגוריה
        //Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userName);
        ////הכנסה לסל של בחירת מתנה
        //Task  AddOrderDal(OrderItem item,int userId);
        //Task <Order> AddBucket(Order order);
        //public Task<Order> GetBucket(int userId);

        ////מחיקת מתנה (לפני רכישה
        //Task<string> removeGiftFromCart(int giftId, int userId);
        ////משתמש לקוח/רק לאחר אישור הקניה המתנות נרכשות בפועל
        //Task<string> purchaseGiftFromCart(int OrderId);
        ////כל מתנה מי הזוכה
        //Task<List<object>> GetWinners();
        //Task<User> AuthenticateDal(User userLogin);
    }
}