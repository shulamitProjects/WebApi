using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.BLL
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        //-------------------------------------------------------------------------------
        Task<User> GetUserById(int userId);
        //-------------------------------------------------------------------------------
        Task<User> AddUser(User u);
        //-------------------------------------------------------------------------------
        Task<bool> Login(string userEmail, string userPassword);
        //-------------------------------------------------------------------------------
        public Task<User> Authenticate(UserLoginDTO userLogin);

        //-------------------------------------------------------------------------------
        Task<bool> DeleteUser(int ID);
        //-------------------------------------------------------------------------------

        //Task<bool> AddGiftToCart(int GiftID, string userName);
        Task<bool> AddOrderItem(OrderItem orderItem, User user);

        //-------------------------------------------------------------------------------

        Task<List<object>> GetGiftOrderIds();
        Task<List<object>> GetGiftOrderIdsById(User user);
        Task<List<object>> GetGiftOrderIdsBygiftId(int giftId);

        //-------------------------------------------------------------------------------
        Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userName);
        //-------------------------------------------------------------------------------
        Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userName);
        //-------------------------------------------------------------------------------
        Task<bool> PurchaseGiftFromCart(int OrderId);
        //-------------------------------------------------------------------------------
        Task<bool> RemoveGiftFromCart(OrderItem orderItem, User user);
        //-------------------------------------------------------------------------------
        Task<List<object>> GetWinners();
        //-------------------------------------------------------------------------------

        ////לקבל רשימת משתמשים
        // public Task<List<User>> GetUserBLL();
        ////סינון לפי ID
        //public Task<User> getUserById(int id);
        ////מסך כניסה
        // //public Task<User> login(UserLoginDTO userLogin);
        ////מסך רישום
        // public Task<int> CreateUser(User userRegister);
        ////צפיה ברשימת המתנות, מיון לפי מחיר
        // public Task<List<Gift>> GetGiftsByUserOrderedByPrice(string price);
        ////צפיה ברשימת המתנות, מיון לפי קטגוריה
        // public Task<List<Gift>> GetGiftsByUserOrderedByCategory(string category);
        ////הכנסה לסל של בחירת מתנה
        //public Task AddOrder(OrderItemDTO orderItem, int userId);
        ////הוספת סל
        //public Task<Order> AddBucket(OrderDTO order);
        //public Task<Order> GetBucket(int userId);
        ////מחיקת מתנה (לפני רכישה
        //public Task<string> removeGiftFromCart(int giftId, int userId);
        ////משתמש לקוח/רק לאחר אישור הקניה המתנות נרכשות בפועל
        // public Task<string> purchaseGiftFromCart(int OrderId);

        // public Task<User> AuthenticateDal(User userLogin);
    }
}
