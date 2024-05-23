using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public class PresentDAL : IPresentDAL//מתחבר למסד הנתונים בפועל
    {
        private readonly OctionContext _ordersContext;//יצירת משתנה פרטי - מקובל לשים קו תחתי
        private readonly ILogger<Order> _logger;

        public PresentDAL(OctionContext octionContext, ILogger<Order> logger)//מקבלים ב DI 
        {
            _ordersContext = octionContext ?? throw new ArgumentNullException(nameof(octionContext));//יצירת משתנה ואם לא מצא-זריקת שגיאה
            _logger = logger;
        }
        //צפיה ברשימת המתנות
        public async Task<List<Gift>> GetPresentDAL()//לקבל רשימה של כל המתנות
        {
            try
            {
            List<Gift> g  =  await _ordersContext.Gift.ToListAsync();
            return g;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //סינון מתנה לפי שם מתנה
        public async Task<Gift> GetByPresentNameDAL(string presentName)
        {
            try
            {
            var present =  _ordersContext.Gift.FirstOrDefault(e=>e.Name == presentName);
            return present;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //סינון מתנה לפי שם תורם
        public async Task<List<Gift>> GetByDonorNameDAL(int donorId)
        {
            try
            {
               List<Gift>gifts= await _ordersContext.Gift.Where(g => g.DonorId == donorId).ToListAsync();
            //var giftName = (from g in _ordersContext.Gift
            //                join d in _ordersContext.Donor on g.DonorId equals d.DonorId
            //                where d.LastName == DonorName
            //                select g.Name).FirstOrDefault();
                return gifts;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //סינון מתנה לפי מס' רוכשים
        public async Task<List<string>> GetByNumberOfPurchasesDAL(int numOfPurchases)
        {
            try
            {
            var result = from g in _ordersContext.Gift
                         join oi in _ordersContext.OrderItem on g.GiftId equals oi.GiftId
                         join o in _ordersContext.Order on oi.OrderId equals o.OrderId
                         group g by new { g.GiftId, g.Name } into grp
                         where grp.Count() == numOfPurchases
                         select grp.Key.Name;
            return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //מחיר כרטיס הגרלה
        public async Task<List<Gift>> GetPresentPriceDAL(int prices)
        {
            try
            {
            var present = _ordersContext.Gift.Where(p => p.Price == prices);
            return await present.ToListAsync(); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //public async Task<List<Gift>> AddPresentDal(Gift present)
        //{
        //    await _ordersContext.Gift.AddAsync(present);
        //    await _ordersContext.SaveChangesAsync();

        //    int len = present.Donor.Count;
        //    var donors = new List<Donor>();
        //    for (int i = 0; i < len; i++)
        //    {
        //        donors = await _ordersContext.Donor
        //        .Where(d => d.DonorId == present.Donor[i])
        //        .ToListAsync();
        //        i++;
        //    }
        //הוספת מתנה
        public async Task<string> AddPresentDAL(Gift gift)
        {
            try
            {
                _ordersContext.Gift.AddAsync(gift);
                await _ordersContext.SaveChangesAsync();
                return $"added present {gift.Name}";
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }

        //עדכון מתנה
        public async Task<string> UpdatePresentDAL(Gift gift)
        {
            _ordersContext.Gift.Update(gift);
            await _ordersContext.SaveChangesAsync();
            return $"updated present {gift.Name}";
        }
        //מחיקת מתנה
        public async Task<string> DeletePresentDAL(int id)
        {
            try
            {
            Gift g = await _ordersContext.Gift.FindAsync(id);
            _ordersContext.Gift.Remove(g);
            await this._ordersContext.SaveChangesAsync();
            return $"present {g.Name} was Deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
    }
}
