using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.BLL
{
    public interface IDonatorService
    {
        public Task<List<Donor>> GetDonorBLL();//צפיה ברשימת התורמים
        public Task<Donor> getDonorById(int id);//סינון תורם לפי ID
        public Task<List<Donor>> GetByNameDonorBLL(string name);//סינון תורם לפי שם
        public Task<string> GetByPresentDonorBLL(string GiftName);//סינון תורם לפי מתנה
        public Task<List<Donor>> GetByEmailDonorBLL(string email);//סינון תורם לפי מייל
        public Task<string> AddDonorBLL(Donor donor);//הוספת תורם
        public Task<string> UpdateDonorBLL(Donor donor);//עדכון תורם
        public Task<string> DeleteDonorBLL(int ID);//מחיקת תורם
    }
}
