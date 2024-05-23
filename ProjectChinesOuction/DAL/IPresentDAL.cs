
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public interface IPresentDAL
    {
        public Task<List<Gift>> GetPresentDAL();
        public Task<Gift> GetByPresentNameDAL(string name);
        public Task<List<Gift>> GetByDonorNameDAL(int donorId);
        public Task<List<string>> GetByNumberOfPurchasesDAL(int numOfPurchases);
        public Task<List<Gift>> GetPresentPriceDAL(int prices);
        public Task<string> AddPresentDAL(Gift gift);
        public Task<string> UpdatePresentDAL(Gift gift);
        public Task<string> DeletePresentDAL(int ID);
    }
}
