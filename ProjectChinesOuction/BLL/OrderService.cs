using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAL _ordersDAL;

        public OrderService(IOrderDAL ordersDAL)
        {
            _ordersDAL = ordersDAL ?? throw new ArgumentNullException(nameof(_ordersDAL));
        }

        public async Task<List<OrderItem>> GetGiftsOrderedByUser(int userId)
        {
            return await _ordersDAL.GetGiftsOrderedByUser(userId);
        }
        //public async Task<List<Gift>> AddOrder(int userId, int presentId)
        //{
        //    return await _ordersDAL.AddOrderDal(userId, presentId);
        //}
        public async Task<List<Gift>> GetPresentOrderedByPrice()
        {
            return await _ordersDAL.GetPresentOrderedByPrice();
        }
        public async Task<List<Gift>> GetGiftsOrderedByCount()
        {
            return await _ordersDAL.GetGiftsOrderedByCount();
        }

        public async Task<List<OrderItem>> GetThePurchasesForEachPresent(int presentId)
        {
            return await _ordersDAL.GetThePurchasesForEachPresentAsync(presentId);
        }
        public async Task<List<User>> GetTheAllPurchases()
        {
            return await _ordersDAL.GetTheAllPurchases();
        }
    }
}
