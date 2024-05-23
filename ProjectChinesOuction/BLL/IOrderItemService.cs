

using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public interface IOrderItemService
    {
        public Task<int> AddPresentToCart(OrderItem present);
        public Task<int> DeletePresentFromCart(int id);
        public Task<List<OrderItem>> GetPresentsOrder();
        public Task<List<OrderItem>> GetThePurchasesForEachPresent(int presentId);
        //public Task<List<Gift>> SortByTheMostPurchasedPresent();
        //public Task<List<Gift>> SortByTheMostExpensivePresent();

    }
}
