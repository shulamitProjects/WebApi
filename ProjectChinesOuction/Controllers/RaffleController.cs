using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.Models;
using ProjectChinesOuction.Models.DTOs;

namespace ProjectChinesOuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : ControllerBase
    {
        private readonly IRaffleService _raffleService;
        private readonly IMapper _mapper;
        public RaffleController(IRaffleService raffleService, IMapper mapper)
        {
            this._raffleService = raffleService;
            this._mapper = mapper;
        }

        //דו"ח הגרלה, הכנסה המשתמשים לרשימה
        [HttpGet]
        [Route("RaffleReport")]
        public async Task<ActionResult<List<RaffleDTO>>> RaffleReport()
        {
            try
            {
                var report = await _raffleService.RaffleReport();
                var _report = _mapper.Map<List<RaffleDTO>>(report);
                return Ok(_report);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the RaffleReport function");
            }
        }
        //הזוכים בהגרלות
        [HttpPost]
        [Route("RaffleWinners")]
        public async Task<ActionResult<User>> RaffleWinners([FromBody] PresentDTO present)
        {
            try
            {
                var _present = _mapper.Map<Gift>(present);
                //var _present = _mapper.Map<Present>(present);
                var winner = await _raffleService.RaffleWinner(_present);
                return Ok(winner);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the RaffleWinners function");
            }
        }
    }
}