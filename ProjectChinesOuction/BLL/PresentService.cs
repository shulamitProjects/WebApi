using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;
using System.Xml.Linq;

namespace ProjectChinesOuction.BLL
{
    public class PresentService : IPresentService//יורש
    {
        private readonly IPresentDAL _presentDAL;
        public PresentService(IPresentDAL presentDAL) 
        {
          this._presentDAL = presentDAL??throw new ArgumentNullException(nameof(presentDAL));
        }
        //צפיה ברשימת המתנות
        public async Task<List<Gift>> GetPresentBLL()
        {
           return await _presentDAL.GetPresentDAL();
        }
        //סינון מתנה לפי שם מתנה
        public async Task<Gift> GetByNamePresentBLL(string Name)
        {
            return await _presentDAL.GetByPresentNameDAL(Name);
        }
        //סינון מתנה לפי שם תורם
        public async Task<List<Gift>> GetByDonorNameBLL(int donorId)
        {
            return await _presentDAL.GetByDonorNameDAL(donorId);
        }
        //סינון מתנה לפי מס' רוכשים
        public async Task<List<string>> GetByNumberOfPurchasesBLL(int numOfPurchases)
        {
            return await _presentDAL.GetByNumberOfPurchasesDAL(numOfPurchases);
        }
        //מחיר כרטיס הגרלה
        public async Task<List<Gift>> GetPresentPriceBLL(int prices)
        {
            return await _presentDAL.GetPresentPriceDAL(prices);
        }
        //הוספת מתנה
        public async Task<string> AddPresentBLL(Gift gift)
        {
            return await _presentDAL.AddPresentDAL(gift);
        }
        //עדכון מתנה
        public async Task<string> UpdatePresentBLL(Gift gift)
        {
            return await _presentDAL.UpdatePresentDAL(gift);
        }
        //מחיקת מתנה
        public async Task<string> DeletePresentBLL(int ID)
        {
            return await _presentDAL.DeletePresentDAL(ID);
        }
    }
}
