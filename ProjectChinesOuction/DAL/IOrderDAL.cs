using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public interface IOrderDAL
    {
        public Task<List<OrderItem>> GetThePurchasesForEachPresentAsync(int presentId);//קבל את הרכישות עבור כל מתנה
        public Task<List<User>> GetTheAllPurchases();//קבל את הרכישות עבור כל מתנה

        public Task<List<Gift>> GetPresentOrderedByPrice();//מיין לפי המתנה היקרה ביותר
        public Task<List<Gift>> GetGiftsOrderedByCount();//מיין לפי המתנה הנרכשת ביותר
        public Task<List<OrderItem>> GetGiftsOrderedByUser(int userId);//צפיה בפרטי הרוכשים

        //public Task<List<Gift>> AddOrderDal(int userId, int presentId);
    }
}
