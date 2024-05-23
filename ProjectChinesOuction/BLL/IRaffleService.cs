using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public interface IRaffleService
    {
        public Task<User> RaffleWinner(Gift present);
        public Task<List<Raffle>> RaffleReport();
    }
}
