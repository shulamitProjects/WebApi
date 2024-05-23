using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectChinesOuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatorController : ControllerBase
    {
        private readonly IDonatorService _donator;
        IMapper _mapper;


        public DonatorController(IDonatorService donator, IMapper _mapper)
        {
            this._donator = donator ?? throw new ArgumentNullException(nameof(donator));
            this._mapper = _mapper;
        }

        //צפיה ברשימת התורמים
        // GET: api/<DonatorController>
        [HttpGet]
        public async Task<ActionResult<List<Donor>>> Get()
        {
            return await _donator.GetDonorBLL();
        }
        //סינון תורם לפי ID
        // GET api/<DoenaterController>/5
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Donor>> GetById(int id)
        {
            try
            {
                return await _donator.getDonorById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
        //סינון תורם לפי שם
        // GET api/<DonatorController>/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<Donor>>> GetDonarByName(string name)
        {
            return await _donator.GetByNameDonorBLL(name);
        }
        //סינון תורם לפי מתנה
        // GET api/<DonatorController>/{present}
        [HttpGet("present/{present}")]
        public async Task<ActionResult<string>> GetByPresentDonor(string GiftName)
        {
            return await _donator.GetByPresentDonorBLL(GiftName);
        }
        //סינון תורם לפי מייל
        // GET api/<DonatorController>/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<List<Donor>>> GetDonarByEmail(string email)
        {
            return await _donator.GetByEmailDonorBLL(email);
        }
        //הוספת תורם
        // POST api/<DonatorController>
        [HttpPost("addDonor")]
        public async Task Post(DonorDTO donor)
        {
            Donor d = _mapper.Map<Donor>(donor);
            await _donator.AddDonorBLL(d);
        }
        //עדכון תורם
        // PUT api/<DonatorController>/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, DonorDTO donor)
        {
            Donor d=_mapper.Map<Donor>(donor);
            return await _donator.UpdateDonorBLL(d);
        }

        //מחיקת תורם
        // DELETE api/<DonatorController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await this._donator.DeleteDonorBLL(id);
        }
    }
}