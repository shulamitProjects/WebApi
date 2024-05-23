using Microsoft.EntityFrameworkCore;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public class RaffleDAL : IRaffleDAL
    {
        private readonly OctionContext _actionContext;

        public RaffleDAL(OctionContext actionContext)
        {
            this._actionContext = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
        }
        //דוח הגרלה
        public async Task<List<Raffle>> RaffleReportAsync()
        {

            return await _actionContext.Raffle.ToListAsync();
        }

        //משתמש ההנהלה עובר מתנה מתנה ומבצע עליה הגרלה מתוך רשימת הרוכשים
        public async Task<User> RaffleWinnerAsync(Gift present)
        {
            List<OrderItem> allPurchasersForThisPresent = new List<OrderItem>();

            var thereIsAlreadyWinner = await _actionContext.Raffle.FirstOrDefaultAsync(r => r.PresentId == present.GiftId);
            if (thereIsAlreadyWinner != null)
                return null;
            var purchasersForThisPresent = await _actionContext.OrderItem.Where(po => po.GiftId == present.GiftId).ToListAsync();
            purchasersForThisPresent.ForEach(item =>
            {
                if (item != null)
                {
                    if (item.Count > 1)
                    {
                        for (var i = 0; i < item.Count; i++)
                            allPurchasersForThisPresent.Add(item);
                    }
                    else
                        //allPurchasersForThisPresent = purchasersForThisPresent;
                        allPurchasersForThisPresent.Add(item);
                }
            }
            );
            if (allPurchasersForThisPresent.Count != 0)
            {
                Random r = new Random();
                int index = r.Next(allPurchasersForThisPresent.Count);
                var orderId = allPurchasersForThisPresent[index].OrderId;
                var winnerOrder = await _actionContext.Order.FirstOrDefaultAsync(o => o.OrderId == orderId);
                var user = await _actionContext.User.FirstOrDefaultAsync(u => u.UserId == winnerOrder.UserId);
                await _actionContext.Raffle.AddAsync(new Raffle() { PresentId = present.GiftId, UserId = user.UserId });

                await _actionContext.SaveChangesAsync();
                return user;
            }
            return null;
        }
    }
}

