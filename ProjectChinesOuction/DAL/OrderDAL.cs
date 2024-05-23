using ProjectChinesOuction.Models;
using System.Collections.Generic;

namespace ProjectChinesOuction.DAL
{
    public class OrderDAL : IOrderDAL
    {
        private readonly OctionContext _octionContext;
        public OrderDAL(OctionContext octionContext)
        {
            _octionContext = octionContext ?? throw new ArgumentNullException(nameof(_octionContext));
        }
        //מיון לפי המתנה הנרכשת ביותר
        public async Task<List<Gift>> GetGiftsOrderedByCount()
        {
            var result = from oi in _octionContext.OrderItem
                         join g in _octionContext.Gift on oi.GiftId equals g.GiftId
                         group oi by oi.GiftId into grp
                         orderby grp.Count() descending
                         select grp.FirstOrDefault().Gift;
            return await result.ToListAsync();
        }
        //קבל הזמנת מתנות לפי משתמש
        public async Task<List<OrderItem>> GetGiftsOrderedByUser(int userId)
        {
            List<OrderItem> orderItems = await _octionContext.OrderItem.Where(o => o.Order.UserId==userId).ToListAsync();

            //var result = from g in _octionContext.Gift
            //              join oi in _octionContext.OrderItem on g.GiftId equals oi.GiftId
            //              join o in _octionContext.Order on oi.OrderId equals o.OrderId
            //              join u in _octionContext.User on o.UserId equals u.UserId
            //              where u.LastName == userName
            //              select g;

            return  orderItems;
        }
        //מיון, לפי המתנה היקרה ביותר
        public async Task<List<Gift>> GetPresentOrderedByPrice()
        {
            var presents = _octionContext.Gift.OrderBy(p => p.Price);
            return await presents.ToListAsync();
            //var present = _octionContext.Gift.Where(p => p.Name == presentName);
            //return await present.ToListAsync();
        }
        //צפייה ברכישות כרטיסים עבור כל מתנה.
        public async Task<List<OrderItem>> GetThePurchasesForEachPresentAsync(int presentId)
        {
            List<OrderItem> orderItems = await _octionContext.OrderItem.Where(o => o.GiftId == presentId).ToListAsync();
            return orderItems;
        }
        public async Task<List<User>> GetTheAllPurchases()
        {
            List<Order> orderItems = await _octionContext.Order.GroupBy(o=>o.UserId).Select(g=>g.First()).ToListAsync();
            List<User> users = orderItems.Select(o => o.User).ToList();
            return users;
        }

        //private readonly OctionContext _OctionContext;
        //private readonly ILogger<OrderItem> _logger;
        //private readonly IMapper _mapper;

        //public OrderDAL(OctionContext OctionContext, ILogger<OrderItem> logger, IMapper mapper)
        //{
        //    this._OctionContext = OctionContext;
        //    this._logger = logger;
        //    this._mapper = mapper;
        //}

        //public async Task<int> AddPresentToCartAsync(OrderItem present)
        //{
        //    try
        //    {

        //        var order = await _OctionContext.Order.FirstOrDefaultAsync(o => o.OrderId == present.OrderId);
        //        var p = await _OctionContext.Gift.FirstOrDefaultAsync(p => p.GiftId == present.GiftId);

        //        //order = order.Sum + p.Price;
        //        await _OctionContext.OrderItem.AddAsync(present);
        //        await _OctionContext.SaveChangesAsync();
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
        //        var po = await _OctionContext.OrderItem.FirstOrDefaultAsync(po => po.Id == id);
        //        //if (po.IsDraft == true)
        //        //{
        //            var order = await _OctionContext.Order.FirstOrDefaultAsync(o => o.OrderId == po.OrderId);
        //            var p = await _OctionContext.Gift.FirstOrDefaultAsync(p => p.GiftId == po.GiftId);
        //            //order.Sum = order.Sum - p.Price;
        //            _OctionContext.OrderItem.Remove(po);
        //            await _OctionContext.SaveChangesAsync();
        //        return id;
        //        //}
        //        //else
        //        //    return -1;

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Logging from OrderItem, the exception: " + ex.Message, 1);
        //        throw new Exception("Logging from OrderItem, the exception: " + ex.Message);
        //    }
        //}

        //public async Task<List<OrderItem>> GetPresentsOrderAsync()
        //{
        //    //return await _OctionContext.OrderItem.Where(po=> po.IsDraft == true).ToListAsync();
        //    return await _OctionContext.OrderItem.ToListAsync();
        //}

        //public Task<List<OrderItem>> GetThePurchasesForEachPresentAsync(int presentId)
        //{
        //    throw new NotImplementedException();
        //}

        ////Task<List<OrderItem>> IOrderDAL.GetThePurchasesForEachPresentAsync(int presentId)
        ////{
        ////    var presents = _OctionContext.OrderItem.Where(po => po.GiftId == presentId);
        ////    return presents.ToList();
        ////}

        //public Task<List<Gift>>SortByTheMostExpensivePresentAsync()
        //{
        //    var q = _OctionContext.Gift.OrderByDescending(p => p.Price);
        //    return q.ToListAsync();
        //}

        //public async Task<List<Gift>>SortByTheMostPurchasedPresentAsync()
        //{
        //    var allOrderItem = await _OctionContext.OrderItem.ToListAsync();
        //    allOrderItem.ForEach(item =>
        //    {
        //        if (item != null)
        //        {
        //            if (item.Count > 0)
        //            {
        //                for (var i = 0; i < item.Count; i++)
        //                    allOrderItem.Add(item);
        //            }
        //            else
        //                allOrderItem = allOrderItem;
        //        }
        //    });
        //    var presents = await _OctionContext.OrderItem
        //                  .Include(po => po.Gift)
        //                  .Include(po => po.Order)
        //                  //.Where(po => po.Order.IsDraft == false)
        //                  .GroupBy(po => po.Gift.GiftId)
        //                  .OrderByDescending(po => po.Count())
        //                  .Select(po => po.First().Gift)
        //                  .ToListAsync();
        //    return presents;
        //}
        //קבל את הרכישות עבור כל מתנה
        //מיון רכישות לפי המתנה היקרה ביותר
        //מיון רכישות לפני המתנה הנרכשת ביותר
        //לקבל רשימת הזמנות
    }
}
