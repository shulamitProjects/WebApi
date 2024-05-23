
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public class RaffleService : IRaffleService
    {


        private readonly IRaffleDAL _dal;
        public RaffleService(IRaffleDAL dal)
        {
            _dal = dal;
        }

        public async Task<List<Raffle>> RaffleReport()
        {
            return await _dal.RaffleReportAsync();
        }

        public async Task<User> RaffleWinner(Gift present)
        {
            return await _dal.RaffleWinnerAsync(present);
        }
    }
}
