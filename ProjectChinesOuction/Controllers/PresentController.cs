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
    public class PresentController : ControllerBase
    {
        IMapper _mapper;
        private readonly IPresentService _present;

        public PresentController(IPresentService present, IMapper _mapper)
        {
            this._present = present ?? throw new ArgumentNullException(nameof(present));
            this._mapper = _mapper;
        }
        //צפיה ברשימת המתנות
        // GET: api/<PresentController>
        [HttpGet]
        public async Task<ActionResult<List<Gift>>> Get()
        {
            return await _present.GetPresentBLL();
        }
        //סינון מתנה לפי שם מתנה
        // GET api/<PresentController>/{presentName}
        [HttpGet("presentName/{presentName}")]
        public async Task<ActionResult<Gift>> GetByNameAsync(string presentName)
        {
            return await _present.GetByNamePresentBLL(presentName)
;
        }
        //סינון מתנה לפי שם תורם
        // GET api/<PresentController>/{DonorName}
        [HttpGet("DonorName/{donorId}")]
        public async Task<List<Gift>> GetByDonorAsync(int donorId)
        {
            return await _present.GetByDonorNameBLL(donorId);
        }
        //סינון מתנה לפי מס' רוכשים
        // GET api/<PresentController>/{DonorName}
        [HttpGet("numOfPurchases/{numOfPurchases}")]
        public async Task<List<string>> GetBynumOfPurchasesAsync(int numOfPurchases)
        {
            return await _present.GetByNumberOfPurchasesBLL(numOfPurchases);
        }
        //הוספת מתנה
        // POST api/<PresentController>
        [HttpPost("addPresent")]
        public async Task<int> Post(PresentDTO gift)
        {
            //return await _present.AddPresentBLL(gift);
            Gift g = _mapper.Map<Gift>(gift);
            await _present.AddPresentBLL(g);
            return 0;
        }
        //עדכון מתנה
        // PUT api/<PresentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(int id,  PresentDTO p)//[FromBody] Gift gift,
        {
            try
            {
            var present = _mapper.Map<Gift>(p);
            if (ModelState.IsValid)
            {
                return Ok(await _present.UpdatePresentBLL(present));
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch
            {
                return NotFound();
            }
        }

        //מחיקת מתנה
        // DELETE api/<PresentController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await _present.DeletePresentBLL(id);
        }
    }
}
