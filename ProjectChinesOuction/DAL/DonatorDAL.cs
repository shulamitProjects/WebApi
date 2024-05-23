using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{
    public class DonatorDAL : IDonatorDAL//מתחבר למסד הנתונים בפועל
    {
        private readonly OctionContext _octionContext;
        private readonly ILogger<Donor> _logger;

        public DonatorDAL(OctionContext octionContext, ILogger<Donor> logger)
        {
            this._octionContext = octionContext ?? throw new ArgumentNullException(nameof(octionContext));
            _logger = logger;
        }
        //צפיה ברשימת התורמים
        public async Task<List<Donor>> GetDonorDAL()
        {
            return await _octionContext.Donor.ToListAsync();
        }
        //סינון לפי ID
        public async Task<Donor> getDonorById(int id)
        {
            Donor g = await _octionContext.Donor.FirstOrDefaultAsync(i => i.DonorId == id);
            return g;
        }
        //סינון תורם לפי שם
        public async Task<List<Donor>> GetByNameDonorDAL(string name)
        {
            try
            {
            string[] fName = name.Split(" ");
            var result = _octionContext.Donor.Where(d => d.FirstName == fName[0] && d.LastName == fName[1]);
            return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //סינון תורם לפי מתנה
        public async Task<string> GetByPresentDonorDAL(string GiftName)
        {
            try
            {
                var lastName = (from g in _octionContext.Gift
                                join d in _octionContext.Donor on g.DonorId equals d.DonorId
                                where g.Name == GiftName
                                select d.LastName).FirstOrDefault();
                return lastName;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //סינון תורם לפי מייל
        public async Task<List<Donor>> GetByEmailDonorDAL(string email)
        {
            try
            {
            var query = _octionContext.Donor.Where(d => d.Email == email);
            return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
        //הוספת תורם
        public async Task<string> AddDonorDAL(Donor donor)
        {
                await _octionContext.Donor.AddAsync(donor);
                await _octionContext.SaveChangesAsync();
                return $"added donar {donor.FirstName}";
        }

        //עדכון תורם
        public async Task<string> UpdateDonorDAL(Donor donor)
        {
            try
            {
                _octionContext.Donor.Update(donor);
                await _octionContext.SaveChangesAsync();
                return $"updated donar {donor.FirstName} {donor.LastName}";
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }
        //מחיקת תורם
        public async Task<string> DeleteDonorDAL(int ID)
        {
            try
            {
                Donor d = await this._octionContext.Donor.FindAsync(ID);
                this._octionContext.Donor.Remove(d);
                await this._octionContext.SaveChangesAsync();
                return $"Donar {d.FirstName} was Deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }

        }
    }
    //צפיה ברשימת התורמים
    //סינון תורם לפי ID
    //סינון תורם לפי שם
    //סינון תורם לפי מתנה
    //סינון תורם לפי מייל
    //הוספת תורם
    //עדכון תורם
    //מחיקת תורם
}


