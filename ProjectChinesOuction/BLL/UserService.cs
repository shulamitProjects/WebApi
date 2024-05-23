using AutoMapper;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.BLL
{
    public class UserService : IUserService
    {
        //-------------------------------------------------------------------------------
        private readonly IUserDAL _userDal;

        //-------------------------------------------------------------------------------
        //C-tor
        public UserService(IUserDAL userDal)
        {
            this._userDal = userDal ?? throw new ArgumentNullException(nameof(userDal));

        }
        //-------------------------------------------------------------------------------
        //get
        public async Task<List<User>> GetUsers()
        {
            return await _userDal.GetUsersAsync();
        }
        //-------------------------------------------------------------------------------
        //getById
        public async Task<User> GetUserById(int userId)
        {
            return await _userDal.GetUserByIdAsync(userId);
        }

        //-------------------------------------------------------------------------------
        public async Task<User> AddUser(User u)
        {
            return await _userDal.AddUserAsync(u);
        }
        //-------------------------------------------------------------------------------
        public async Task<bool> Login(string userEmail, string userPassword)
        {
            return await _userDal.LoginAsync(userEmail, userPassword);
        }
        //-------------------------------------------------------------------------------

        public async Task<bool> DeleteUser(int ID)
        {
            return await _userDal.DeleteAsync(ID);

        }
        //-------------------------------------------------------------------------------

        //public async Task<bool> AddGiftToCart(int GiftID, string userName)
        //{
        //    return await _userDal.AddGiftToCartAsync(GiftID, userName);
        //}  
        public async Task<bool> AddOrderItem(OrderItem orderItem, User user)
        {
            return await _userDal.AddOrderItemAsync(orderItem, user);
        }

        //-------------------------------------------------------------------------------
        public async Task<List<Gift>> GetGiftsByUserOrderedByCategory(string userName)
        {
            return await _userDal.GetGiftsByUserOrderedByCategoryAsync(userName);
        }
        //-------------------------------------------------------------------------------


        public async Task<List<Gift>> GetGiftsByUserOrderedByPrice(string userName)
        {
            return await _userDal.GetGiftsByUserOrderedByPriceAsync(userName);
        }
        //-------------------------------------------------------------------------------


        public Task<bool> PurchaseGiftFromCart(int OrderId)
        {
            return _userDal.PurchaseGiftFromCartAsync(OrderId);
        }
        //-------------------------------------------------------------------------------


        public async Task<bool> RemoveGiftFromCart(OrderItem orderItem, User user)
        {
            return await _userDal.RemoveGiftFromCartAsync(orderItem, user);
        }
        //-------------------------------------------------------------------------------

        public async Task<List<object>> GetGiftOrderIds()
        {
            return await _userDal.GetGiftOrderIdsAsync();
        }
        //-------------------------------------------------------------------------------
        public async Task<List<object>> GetGiftOrderIdsById(User user)
        {
            return await _userDal.GetGiftOrderIdsByIdAsync(user);
        }
        //-------------------------------------------------------------------------------
        public async Task<List<object>> GetGiftOrderIdsBygiftId(int giftId)
        {
            return await _userDal.GetGiftOrderIdsBygiftIdAsync(giftId);
        }


        //-------------------------------------------------------------------------------

        public Task<List<object>> GetWinners()
        {
            return _userDal.GetWinnersAsync();
        }
        //-------------------------------------------------------------------------------

        public async Task<User> Authenticate(UserLoginDTO userLogin)
        {
            return await _userDal.AuthenticateDal(userLogin);
        }

    }
    //    private readonly IUserDAL _userDAL;
    //    private readonly IMapper _mapper;

    //    public UserService(IUserDAL userDAL, IMapper mapper)
    //    {
    //        _userDAL = userDAL ?? throw new ArgumentNullException(nameof(_userDAL));
    //        _mapper = mapper;

    //    }

    //    public async Task<List<User>> GetUserBLL()
    //    {
    //        return await _userDAL.GetUserDAL();
    //    }

    //    public async Task<User> getUserById(int id)
    //    {
    //        return await _userDAL.getUserById(id);
    //    }

    //    //Task<User> IUserService.login(UserLoginDTO userLogin)
    //    //{
    //    //    return _userDAL.login(userLogin);
    //    //}

    //    public async Task<int> CreateUser(User user)
    //    {
    //        return await _userDAL.CreateUser(user);
    //    }

    //    public async Task<List<Gift>> GetGiftsByUserOrderedByPrice(string price)
    //    {
    //        return await _userDAL.GetGiftsByUserOrderedByPrice(price);
    //    }

    //    public async Task<List<Gift>> GetGiftsByUserOrderedByCategory(string category)
    //    {
    //        return await _userDAL.GetGiftsByUserOrderedByCategory(category);
    //    }

    //    //public async Task<string> AddGiftToCart(int GiftID, int userID)
    //    //{
    //    //    return await _userDAL.AddGiftToCart(GiftID, userID);
    //    //}
    //    public async Task AddOrder(OrderItemDTO item, int userId)
    //    {
    //        OrderItem orderItem= _mapper.Map<OrderItem>(item);
    //         await _userDAL.AddOrderDal(orderItem, userId);
    //    }

    //    public async Task<Order> AddBucket(OrderDTO orderdto)
    //    {
    //        Order order = _mapper.Map<Order>(orderdto);
    //        return await _userDAL.AddBucket(order);
    //    }
    //    public async Task<Order> GetBucket(int userId)
    //    {
    //        return await _userDAL.GetBucket(userId);
    //    }

    //    public async Task<string> removeGiftFromCart(int giftId, int userId)
    //    {
    //        return await _userDAL.removeGiftFromCart(giftId, userId);
    //    }

    //    public Task<string> purchaseGiftFromCart(int OrderId)
    //    {
    //        return _userDAL.purchaseGiftFromCart(OrderId);
    //    }

    //    public Task<List<object>> GetWinners()
    //    {
    //        return _userDAL.GetWinners();
    //    }

    //    public Task<User> AuthenticateDal(User userLogin)
    //    {
    //        throw new NotImplementedException();
    //    }
}

