using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public interface IRaffleDAL
    {
        public Task<User> RaffleWinnerAsync(Gift present);
        public Task<List<Raffle>> RaffleReportAsync();
    }
}
