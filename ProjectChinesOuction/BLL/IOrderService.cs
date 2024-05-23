using ProjectChinesOuction.Models;
namespace ProjectChinesOuction.BLL
{
    public interface IOrderService
    {
        public Task<List<Gift>> GetPresentOrderedByPrice();
        public Task<List<OrderItem>> GetGiftsOrderedByUser(int userId);
        public Task<List<Gift>> GetGiftsOrderedByCount();
        public Task<List<OrderItem>> GetThePurchasesForEachPresent(int presentId);
        public Task<List<User>> GetTheAllPurchases();

        //public Task<List<Gift>> AddOrder(int GiftID, int userID);
    }
}
