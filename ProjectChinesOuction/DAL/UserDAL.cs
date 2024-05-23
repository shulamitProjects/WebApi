using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Linq;
namespace ProjectChinesOuction.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly OctionContext _dbSale;
        private readonly ILogger<Gift> _logger;


        //-------------------------------------------------------------------------------
        //C-tor
        public UserDAL(OctionContext dbSale)
        {
            this._dbSale = dbSale ?? throw new ArgumentNullException(nameof(dbSale));

        }
        //-------------------------------------------------------------------------------
        //הצגת המשתמשים
        public async Task<List<User>> GetUsersAsync()
        {
            List<User> users = await _dbSale.User.ToListAsync();
            if (users.Count == 0)
            {
                throw new Exception("אין משתמשים במערכת ");
            }
            return users;
        }
        //הצגת משתמש לפי id
        public async Task<User> GetUserByIdAsync(int userId)
        {

            User user = await _dbSale.User.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("משתמש זה  לא רשום למערכת ");
            }
            return user;

        }
        //-------------------------------------------------------------------------------
        //Register
        public async Task<User> AddUserAsync(User u)
        {

            if (_dbSale.User.Any(x => x.Email == u.Email && x.Password == u.Password))
            {
                throw new Exception("כבר נמצא כזה אדם");
            }
            else
            {
                await _dbSale.User.AddAsync(u);
                await this._dbSale.SaveChangesAsync();

                Order o = new Order();
                o.Date = new DateTime();
                var currentUser = _dbSale.User.FirstOrDefault(o => o.Email.ToLower() ==
                u.Email.ToLower() && o.Password == u.Password);
                o.UserId = currentUser.UserId;
                await _dbSale.Order.AddAsync(o);
                await _dbSale.SaveChangesAsync();

                Console.WriteLine($"User {u.FirstName + ' ' + u.LastName} Created");
                return u;
            }
        }
        //-- login ------------------------------------------------------------------------------------
        public async Task<bool> LoginAsync(string userEmail, string userPassword)
        //GetUserByEmailAndPassword
        {
            var user = await _dbSale.User.Where(u => u.Email.ToLower() == userEmail.ToLower() && u.Password == userPassword).FirstOrDefaultAsync();
            if (user == null)
            {
                //throw new Exception($"לא קיים משתמש זה {userEmail}");
                return false;
            }
            return true;

        }
        //-- AuthenticateDal -----------------------------------------------------------------------

        public async Task<User> AuthenticateDal(UserLoginDTO userLogin)
        {
            var currentUser = _dbSale.User.FirstOrDefault(o => o.Email.ToLower() ==
            userLogin.Email.ToLower() && o.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
        //-- Delete -----------------------------------------------------------------------
        public async Task<bool> DeleteAsync(int ID)
        {
            var user = await _dbSale.User.FirstOrDefaultAsync(u => u.UserId == ID);
            if (user != null)
            {
                var qery = await _dbSale.Order.FirstOrDefaultAsync(u => u.UserId == user.UserId);//Orderr\OrderItem
                if (qery != null)
                {
                    throw new Exception($"לא חוקי למחוק משתמש שהזמין מתנה ");
                }
                else
                {
                    this._dbSale.User.Remove(user);
                    await this._dbSale.SaveChangesAsync();
                    Console.WriteLine($"User {user.FirstName + ' ' + user.LastName} Deleted");
                    return true;
                }

            }
            //throw new Exception($"לא נמצא אדם זה ");
            return false;
        }
        //-------------------------------------------------------------------------------
        //public async Task<string> UpdateAsync(User u)
        //{
        //    _dbSale.Users.Update(u);
        //    await this._dbSale.SaveChangesAsync();

        //    //try-cha
        //    return $"User {u.UserFirstName + ' ' + u.UserLastName} Updeted";
        //}
        //-------------------------------------------------------------------------------

        //הוספת מוצר לסל
        //public async Task<bool> AddGiftToCartAsync(int giftId, string userName)
        //{
        //    // Get the UserId for the specified UserName
        //    var user = await _dbSale.Users.FirstOrDefaultAsync(u => u.UserLastName == userName);//?
        //    if (user == null)
        //    {
        //        Console.WriteLine("User with UserName {userName} does not exist.");
        //        return false;
        //    }

        //    // Add a new row to the Orders table with the specified UserId
        //    var order = new Order { UserId = user.UserId, };//!
        //    _dbSale.Orders.Add(order);
        //    _dbSale.SaveChanges();

        //    // Add a new row to the OrderItem table with the specified GiftId and OrderId
        //    var orderItem = new OrderItem { GiftId = giftId, OrderId = order.OrderId, };// OrderItemStatus = EnumOrderStatus.inCart
        //    _dbSale.OrderItem.Add(orderItem);
        //    _dbSale.SaveChanges();

        //    Console.WriteLine($"Item added to {userName}'s cart successfully.");
        //    return true;
        //}
        public async Task<bool> AddOrderItemAsync(OrderItem orderItem, User user)
        {

            //var o = orderItem;
            try
            {
                var order = await _dbSale.Order.FirstOrDefaultAsync(o => o.UserId == user.UserId);
                var gift = await _dbSale.Gift.FirstOrDefaultAsync(g => g.GiftId == orderItem.GiftId);
                var gif = await _dbSale.OrderItem.FirstOrDefaultAsync(gi => gi.GiftId == orderItem.GiftId && gi.OrderId == order.OrderId);


                orderItem.OrderId = order.OrderId;


                if (order != null && gift != null)
                    order.sum += (int)gift.Price * orderItem.Count;
                _dbSale.Order.Update(order);

                if (gif != null)
                {
                    //var qua = await _dbSale.OrderItem.FirstOrDefaultAsync(q => q.OrderId == orderItem.OrderId);
                    //qua.Quantity += orderItem.Quantity;
                    gif.Count += orderItem.Count;


                    //orderItem.Quantity += o.Quantity;
                    //await _dbSale.OrderItem.Q
                    //await _dbSale.SaveChangesAsync();
                    _dbSale.OrderItem.Update(gif);

                    await _dbSale.SaveChangesAsync();

                }
                else
                {
                    await _dbSale.OrderItem.AddAsync(orderItem);

                    await _dbSale.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }

        //-------------------------------------------------------------------------------
        //מחיקת מוצר מהסל
        //public async Task<bool> RemoveGiftFromCartAsync(int giftId, int userId)
        //{
        //    var orderItem = await _dbSale.OrderItem.FirstOrDefaultAsync(oi => oi.GiftId == giftId && oi.Order.UserId == userId);
        //    if (orderItem == null)
        //    {
        //        throw new Exception($"gift with giftId {giftId} does not exist in your cart.");

        //    }

        //    var order = await _dbSale.Orders.FindAsync(orderItem.OrderId);
        //    if (order == null)
        //    {
        //        throw new Exception($"order with orderId {orderItem.OrderId} does not exist.");
        //    }

        //    //if (order.OrderStatus != EnumOrderStatus.inCart)//! && orderItem.OrderItemStatus != EnumOrderStatus.inCart
        //    //{
        //    //    throw new Exception($"you can not remove an order that's not in your cart");
        //    //}

        //    _dbSale.OrderItem.Remove(orderItem);
        //    _dbSale.Orders.Remove(order);
        //    _dbSale.SaveChanges();

        //    Console.WriteLine($"gift with giftId {giftId} was removed successfully from user with userId {userId}'s cart.");
        //    return true;
        //}
        public async Task<bool> RemoveGiftFromCartAsync(OrderItem orderItem, User user)
        {

            //var o = orderItem;
            try
            {
                var order = await _dbSale.Order.FirstOrDefaultAsync(o => o.UserId == user.UserId);
                var gift = await _dbSale.Gift.FirstOrDefaultAsync(g => g.GiftId == orderItem.GiftId);
                var gif = await _dbSale.OrderItem.FirstOrDefaultAsync(gi => gi.GiftId == orderItem.GiftId && gi.OrderId == order.OrderId);


                orderItem.OrderId = order.OrderId;


                if (order != null && gift != null)
                    order.sum += (int)gift.Price * orderItem.Count;
                _dbSale.Order.Update(order);

                if (gif != null)
                {

                    if (gif.Count > orderItem.Count && gif.Count > 1)
                    {
                        gif.Count -= orderItem.Count;

                        _dbSale.OrderItem.Update(gif);

                        await _dbSale.SaveChangesAsync();
                    }
                    else if (gif.Count == 1)
                    {
                        _dbSale.OrderItem.Remove(gif);

                        await _dbSale.SaveChangesAsync();

                    }


                }
                else
                {
                    throw new Exception("לא קיים מוצ");
                }


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }


        //-------------------------------------------------------------------------------
        //•	ניהול רכישות:לפי המתנה היקרה ביותר
        public async Task<List<Gift>> GetGiftsByUserOrderedByPriceAsync(string userName)//? email
        {
            var result = from g in _dbSale.Gift
                         join oi in _dbSale.OrderItem on g.GiftId equals oi.GiftId
                         join o in _dbSale.Order on oi.OrderId equals o.OrderId
                         join u in _dbSale.User on o.UserId equals u.UserId
                         where u.LastName == userName
                         select g;
            return await result.OrderBy(g => g.GiftId).ToListAsync();
        }
        //-------------------------------------------------------------------------------

        //חיפוש מתנה לפי... קטגוריה
        public async Task<List<Gift>> GetGiftsByUserOrderedByCategoryAsync(string userName)
        {
            var result = from g in _dbSale.Gift
                         join oi in _dbSale.OrderItem on g.GiftId equals oi.GiftId
                         join o in _dbSale.Order on oi.OrderId equals o.OrderId
                         join u in _dbSale.User on o.UserId equals u.UserId
                         where u.LastName == userName
                         select g;

            return await result.OrderBy(p => p.Category).ToListAsync();
        }

        //-------------------------------------------------------------------------------
        //•	משתמש לקוח/רק לאחר אישור הקניה המתנות נרכשות בפועל.
        public async Task<bool> PurchaseGiftFromCartAsync(int OrderId)
        {
            var order = await _dbSale.Order.FindAsync(OrderId);
            if (order == null)
            {
                throw new Exception($"Order with OrderId {OrderId} does not exist.");
            }

            //order.OrderStatus = EnumOrderStatus.Purchased;//!!
            _dbSale.Order.Update(order);
            _dbSale.SaveChanges();

            Console.WriteLine($"Order number {OrderId} was ordered successfully.");
            return true;
        }
        //-------------------------------------------------------------------------------
        //צפייה ברכישות כרטיסים עבור כל מתנה|ניהול רכישות	
        public async Task<List<object>> GetGiftOrderIdsAsync()
        {
            var result = from oi in _dbSale.OrderItem
                         join g in _dbSale.Gift on oi.GiftId equals g.GiftId
                         group new { oi, g } by oi.GiftId into grp
                         select new
                         {
                             GiftId = grp.Key,
                             OrderIds = string.Join(",", grp.Select(x => x.oi.OrderId)),
                             GiftNames = string.Join(",", grp.Select(x => x.g.Name))
                         };

            return await result.Cast<object>().ToListAsync();
        }
        //-------------------------------------------------------------------------------
        //צפיה בסל 
        public async Task<List<object>> GetGiftOrderIdsByIdAsync(User user)
        {
            var result = from oi in _dbSale.OrderItem
                         join g in _dbSale.Gift on oi.GiftId equals g.GiftId
                         join o in _dbSale.Order on oi.OrderId equals o.OrderId
                         where o.UserId == user.UserId
                         group new { oi, g } by oi.GiftId into grp
                         select new
                         {
                             GiftId = grp.Key,
                             OrderIds = string.Join(",", grp.Select(x => x.oi.OrderId)),
                             GiftNames = string.Join(",", grp.Select(x => x.g.Name))
                         };

            return await result.Cast<object>().ToListAsync();
        }
        //-------------------------------------------------------------------------------
        //צפיה בכל הרוכשים לפי שם מתנה
        public async Task<List<object>> GetGiftOrderIdsBygiftIdAsync(int giftId)
        {
            var result = from oi in _dbSale.OrderItem
                         join g in _dbSale.Gift on oi.GiftId equals g.GiftId
                         where oi.GiftId == giftId
                         group new { oi, g } by oi.GiftId into grp
                         select new
                         {
                             GiftId = grp.Key,
                             OrderIds = string.Join(",", grp.Select(x => x.oi.OrderId)),
                             GiftNames = string.Join(",", grp.Select(x => x.g.Name))
                         };

            return await result.Cast<object>().ToListAsync();
        }

        //-------------------------------------------------------------------------------

        public async Task<List<object>> GetWinnersAsync()
        {
            var result = from w in _dbSale.Winner
                         join g in _dbSale.Gift on w.GiftId equals g.GiftId
                         join u in _dbSale.User on w.UserId equals u.UserId
                         select new
                         {
                             WinnerId = w.UserId,
                             GiftName = g.Name,
                             UserName = u.FirstName + ' ' + u.LastName
                         };

            return await result.Cast<object>().ToListAsync();
        }




        //-------------------------------------------------------------------------------
        //private readonly OctionContext _oactionContext;
        //private readonly ILogger<Order> _logger;

        //public UserDAL(OctionContext oactionContext, ILogger<Order> logger)
        //{
        //    _oactionContext = oactionContext ?? throw new ArgumentNullException(nameof(_oactionContext));
        //    _logger = logger;
        //}
        //        public async Task<List<User>> GetUserDAL()
        //        {
        //            return await _oactionContext.User.ToListAsync();
        //        }
        //        //סינון לפי ID
        //        public async Task<User> getUserById(int id)
        //        {
        //            User g = await _oactionContext.User.FirstOrDefaultAsync(i => i.UserId == id);
        //            return g;
        //        }
        //        ////מסך כניסה
        //        //public async Task<User> login(UserLoginDTO userLogin)
        //        //{
        //        //    try
        //        //    {
        //        //        var user =await _oactionContext.User.FirstOrDefaultAsync(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
        //        //        if (user == null)
        //        //        {
        //        //            return null;
        //        //        }

        //        //        return user;
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //        //        throw new Exception("Logging from donor, the exception: " + ex.Message);
        //        //    }

        //        //}
        //        //מסך רישום
        //        public async Task<int> CreateUser(User userRegister)
        //        {
        //            try
        //            {
        //                var user = new User { Password = userRegister.Password, FirstName = userRegister.FirstName, LastName = userRegister.LastName, PhonNumber = userRegister.PhonNumber, Email = userRegister.Email };
        //                await _oactionContext.User.AddAsync(user);
        //                _oactionContext.SaveChanges();
        //                return user.UserId;
        //                //return $"User{user.UserId} with emailwas created successfully.";
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //                throw new Exception("Logging from donor, the exception: " + ex.Message);
        //            }
        //        }
        //        //צפיה ברשימת המתנות, מיון לפי מחיר
        //        public async Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userName)
        //        {
        //            try
        //            {
        //                var result = from g in _oactionContext.Gift
        //                             join oi in _oactionContext.OrderItem on g.GiftId equals oi.GiftId
        //                             join o in _oactionContext.Order on oi.OrderId equals o.OrderId
        //                             join u in _oactionContext.User on o.UserId equals u.UserId
        //                             where u.LastName == userName
        //                             select g;

        //                return await result.OrderBy(p => p.Price).ToListAsync();
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //                throw new Exception("Logging from donor, the exception: " + ex.Message);
        //            }
        //        }
        //        //צפיה ברשימת המתנות, מיון לפי קטגוריה
        //        public async Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userName)
        //        {
        //            try
        //            {
        //                var result = from g in _oactionContext.Gift
        //                             join oi in _oactionContext.OrderItem on g.GiftId equals oi.GiftId
        //                             join o in _oactionContext.Order on oi.OrderId equals o.OrderId
        //                             join u in _oactionContext.User on o.UserId equals u.UserId
        //                             where u.LastName == userName
        //                             select g;

        //                return await result.OrderBy(p => p.Category).ToListAsync();
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //                throw new Exception("Logging from donor, the exception: " + ex.Message);
        //            }

        //        }
        //        //מחיקת מתנה (לפני רכישה
        //        public async Task<string> removeGiftFromCart(int giftId, int userId)
        //        {
        //            try
        //            {
        //                var orderItem = await _oactionContext.OrderItem.FirstOrDefaultAsync(oi => oi.GiftId == giftId && oi.Order.UserId == userId);
        //                if (orderItem == null)
        //                {
        //                    return $"gift with giftId {giftId} does not exist in your cart.";
        //                }

        //                var order = await _oactionContext.Order.FindAsync(orderItem.OrderId);
        //                if (order == null)
        //                {
        //                    return $"order with orderId {orderItem.OrderId} does not exist.";
        //                }

        //                if (order.isDraft ==true)
        //                {
        //                    return $"you can not remove an order that's not in your cart";
        //                }

        //                _oactionContext.OrderItem.Remove(orderItem);
        //                _oactionContext.Order.Remove(order);
        //                _oactionContext.SaveChanges();

        //                return $"gift with giftId {giftId} was removed successfully from user with userId {userId}'s cart.";
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //                throw new Exception("Logging from donor, the exception: " + ex.Message);
        //            }

        //        }
        //        //הכנסה לסל של בחירת מתנה
        //        //הוספה לסל
        //        public async Task AddOrderDal(OrderItem item,int userId)
        //        {
        //            Order order = await _oactionContext.Order.FirstOrDefaultAsync(o => o.UserId == userId);
        //            order.sum += item.Count;
        //            item.OrderId = order.OrderId;
        //            _oactionContext.Order.Update(order);
        //             await _oactionContext.OrderItem.AddAsync(item);
        //            _oactionContext.SaveChanges();
        //}
        //        public async Task<Order> GetBucket(int userId)
        //        {
        //            return await _oactionContext.Order.FirstOrDefaultAsync(o => o.UserId == userId);
        //            //context
        //            //Order.userid=userid
        //        }

        //        //List<Gift> userPresents = new List<Gift>();

        //        //Order order = await _oactionContext.Order.FirstOrDefaultAsync(o => o.OrderId == item.OrderId)
        //        //order.presentId = item;
        //        //order.Date = DateTime.Now.Date;
        //        ////order.OrderStatus = "inCart";
        //        //await _oactionContext.Order.AddAsync(order);
        //        //await _oactionContext.SaveChangesAsync();

        //        //var userOrders = await _oactionContext.Order.Where(o => o.UserId == userId).ToListAsync();
        //        //foreach (var ord in userOrders)
        //        //{
        //        //    var p = await _oactionContext.Gift.Where(o => o.GiftId == ord.presentId).ToListAsync();
        //        //    userPresents.AddRange(p);
        //        //}
        //        //return userPresents;

        //        //public async Task<string> AddGiftToCart(int GiftID, int userID)
        //        //{
        //        //    try
        //        //    {
        //        //        // Get the UserId for the specified UserName
        //        //        var user = await _oactionContext.User.FirstOrDefaultAsync(u => u.UserId == userID);
        //        //        if (user == null)
        //        //        {
        //        //            return $"User with UserName {userID} does not exist.";
        //        //        }

        //        //        // Add a new row to the Orders table with the specified UserId
        //        //        var order = new Order { UserId = user.UserId, OrderStatus = EnumOrderStatus.inCart };
        //        //        _oactionContext.Order.Add(order);
        //        //        _oactionContext.SaveChanges();

        //        //        // Add a new row to the OrderItem table with the specified GiftId and OrderId
        //        //        var orderItem = new OrderItem { GiftId = GiftID, OrderId = order.OrderId };
        //        //        _oactionContext.OrderItem.Add(orderItem);
        //        //        _oactionContext.SaveChanges();

        //        //        return $"Item added to {userID}'s cart successfully.";
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //        //        throw new Exception("Logging from donor, the exception: " + ex.Message);
        //        //    }
        //        //}

        //        //משתמש לקוח/רק לאחר אישור הקניה המתנות נרכשות בפועל
        //        public async Task<string> purchaseGiftFromCart(int OrderId)
        //        {
        //            try
        //            {
        //                var order = await _oactionContext.Order.FindAsync(OrderId);
        //                if (order == null)
        //                {
        //                    return $"Order with OrderId {OrderId} does not exist.";
        //                }

        //                order.isDraft = true;
        //                _oactionContext.Update(order);
        //                _oactionContext.SaveChanges();

        //                return $"Order number {OrderId} was ordered successfully.";
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
        //                throw new Exception("Logging from donor, the exception: " + ex.Message);
        //            }

        //        }

        //        public Task<List<object>> GetWinners()
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public async Task<User> AuthenticateDal(User userLogin)
        //        {
        //            var currentUser = _oactionContext.User.FirstOrDefault(o => o.FirstName.ToLower() ==
        //            userLogin.FirstName.ToLower() && o.Password == userLogin.Password);
        //            if (currentUser != null)
        //            {
        //                return currentUser;
        //            }
        //            return null;
        //        }

        //        public async Task<Order> AddBucket(Order order)
        //        {
        //            order.isDraft = false;
        //            order.Date = DateTime.Now;
        //order.User = await _oactionContext.User.FirstOrDefaultAsync(u => u.UserId == order.UserId);
        //            await _oactionContext.Order.AddAsync(order);
        //            await _oactionContext.SaveChangesAsync();

        //            return order;
        //        }
    }
}