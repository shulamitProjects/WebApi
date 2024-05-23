
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public interface IPresentService
    {
        public Task<List<Gift>> GetPresentBLL();//יחזיר רשימת מתנות מהמודל
        public Task<Gift> GetByNamePresentBLL(string Name);
        public Task<List<Gift>> GetByDonorNameBLL(int donorId);
        public Task<List<string>> GetByNumberOfPurchasesBLL(int numOfPurchases);
        public Task<List<Gift>> GetPresentPriceBLL(int prices);
        public Task<string> AddPresentBLL(Gift gift);
        public Task<string> UpdatePresentBLL(Gift gift);
        public Task<string> DeletePresentBLL(int ID);
    }
}
