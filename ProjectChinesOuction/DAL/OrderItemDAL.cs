using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public class OrderItemDAL : IOrderItemDAL
    {
        private readonly OctionContext _saleContext;
        private readonly ILogger<OrderItem> _logger;
        private readonly IMapper _mapper;

        public OrderItemDAL(OctionContext saleContext, ILogger<OrderItem> logger, IMapper mapper)
        {
            this._saleContext = saleContext;
            this._logger = logger;
            this._mapper = mapper;
        }

        //public async Task<int> AddPresentToCartAsync(OrderItem present)
        //{
        //    try
        //    {

        //        var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.OrderId == present.OrderId);
        //        var p = await _saleContext.Gift.FirstOrDefaultAsync(p => p.GiftId == present.GiftId);

        //        order.sum = (int)(order.sum + p.Price);
        //        await _saleContext.OrderItem.AddAsync(present);
        //        await _saleContext.SaveChangesAsync();
        //        return present.Id;
        //    }

        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Logging from cart, the exception: " + ex.Message, 1);
        //        throw new Exception("Logging from cart, the exception: " + ex.Message);
        //    }

        //}

        //public async Task<int> DeletePresentFromCartAsync(int id)
        //{
        //    try
        //    {
        //        var po = await _saleContext.OrderItem.FirstOrDefaultAsync(po => po.Id == id);

        //            var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.OrderId == po.OrderId);
        //            var p = await _saleContext.Gift.FirstOrDefaultAsync(p => p.GiftId == po.GiftId);
        //            order.sum = (int)(order.sum - p.Price);
        //            _saleContext.OrderItem.Remove(po);
        //            await _saleContext.SaveChangesAsync();
        //            return id;


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Logging from presentsOrder, the exception: " + ex.Message, 1);
        //        throw new Exception("Logging from presentsOrder, the exception: " + ex.Message);
        //    }
        //}

        public async Task<List<OrderItem>> GetPresentsOrderAsync()
        {
            //return await _saleContext.PresentsOrder.Where(po=> po.IsDraft == true).ToListAsync();
            return await _saleContext.OrderItem.ToListAsync();
        }

        public async Task<List<OrderItem>> GetThePurchasesForEachPresentAsync(int presentId)
        {
            var presents = _saleContext.OrderItem.Where(po => po.GiftId == presentId);
            return presents.ToList();
        }

        //public Task<List<Present>> SortByTheMostExpensivePresentAsync()
        //{
        //    var q = _saleContext.Gift.OrderByDescending(p => p.Price);
        //    return q.ToListAsync();
        //}

        //public async Task<List<Present>> SortByTheMostPurchasedPresentAsync(List<Present> presents)
        //{
        //    var allPresentsOrder = await _saleContext.OrderItem.ToListAsync();
        //    allPresentsOrder.ForEach(item =>
        //    {
        //        if (item != null)
        //        {
        //            if (item.Count > 0)
        //            {
        //                for (var i = 0; i < item.Count; i++)
        //                    allPresentsOrder.Add(item);
        //            }
        //            else
        //                allPresentsOrder = allPresentsOrder;
        //        }
        //    });
        //    var presents = await _saleContext.OrderItem
        //                  .Include(po => po.Gift)
        //                  .Include(po => po.Order)
        //                 // .Where(po => po.Order.IsDraft == false)
        //                  .GroupBy(po => po.Gift.GiftId)
        //                  .OrderByDescending(po => po.Count())
        //                  .Select(po => po.First().Gift)
        //                  .ToListAsync();
        //    return presents;
        //}
    }
}
