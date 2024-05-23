using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public interface IDonatorDAL
    {
        public Task<List<Donor>> GetDonorDAL();//צפיה ברשימת התורמים
        public Task<Donor> getDonorById(int id);//סינון תורם לפי ID
        public Task<List<Donor>> GetByNameDonorDAL(string name);//סינון תורם לפי שם
        public Task<string> GetByPresentDonorDAL(string GiftName);//סינון תורם לפי מתנה
        public Task<List<Donor>> GetByEmailDonorDAL(string email);//סינון תורם לפי מייל
        public Task<string> AddDonorDAL(Donor donor);//הוספת תורם
        public Task<string> UpdateDonorDAL(Donor donor);//עדכון תורם
        public Task<string> DeleteDonorDAL(int ID);//מחיקת תורם
    }
}