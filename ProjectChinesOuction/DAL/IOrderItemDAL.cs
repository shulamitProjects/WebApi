using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public interface IOrderItemDAL
    {
        //public Task<int> AddPresentToCartAsync(OrderItem present);
        //Task<int> AddToPresentCartAsync(Order o);
        //public Task<int> DeletePresentFromCartAsync(int id);
        public Task<List<OrderItem>> GetPresentsOrderAsync();
        public Task<List<OrderItem>> GetThePurchasesForEachPresentAsync(int presentId);
        //public Task<List<Present>> SortByTheMostPurchasedPresentAsync();//מיין לפי המתנה הנרכשת ביותר
        //public Task<List<Present>> SortByTheMostExpensivePresentAsync();//מיין לפי המתנה היקרה ביותר
    }
}
