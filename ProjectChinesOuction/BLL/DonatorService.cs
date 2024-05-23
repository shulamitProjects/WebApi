using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public class DonatorService: IDonatorService
    {
        private readonly IDonatorDAL _donatorDAL;

        public  DonatorService(IDonatorDAL donatorDAL)
        {
            this._donatorDAL = donatorDAL ?? throw new ArgumentNullException(nameof(donatorDAL));
        }
        //צפיה ברשימת התורמים
        public async Task<List<Donor>> GetDonorBLL()
        {
            return await _donatorDAL.GetDonorDAL();
        }
        //סינון לפי ID
        public async Task<Donor> getDonorById(int id)
        {
            return await _donatorDAL.getDonorById(id);
        }
        //סינון תורם לפי שם
        public async Task<List<Donor>> GetByNameDonorBLL(string name)
        {
            return await _donatorDAL.GetByNameDonorDAL(name);
        }
        //סינון תורם לפי מתנה
        public async Task<string> GetByPresentDonorBLL(string GiftName)
        {
            return await _donatorDAL.GetByPresentDonorDAL(GiftName);
        }
        //סינון תורם לפי מייל
        public async Task<List<Donor>> GetByEmailDonorBLL(string email)
        {
            return await _donatorDAL.GetByEmailDonorDAL(email);
        }
        //הוספת תורם
        public async Task<string> AddDonorBLL(Donor donor)
        {
            return await _donatorDAL.AddDonorDAL(donor);
        }
        //עדכון תורם
        public async Task<string> UpdateDonorBLL(Donor donor)
        {
            return await _donatorDAL.UpdateDonorDAL(donor);
        }
        //מחיקת תורם
        public async Task<string> DeleteDonorBLL(int ID)
        {
            return await _donatorDAL.DeleteDonorDAL(ID);
        }
    }
}
