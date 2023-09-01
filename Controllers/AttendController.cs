using FunShareWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FunShareWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendController : ControllerBase
    {
        private readonly FUNShareContext _context;
        public AttendController(FUNShareContext context)
        {
            _context = context;
        }
        // GET: api/<AttendController>
        [HttpGet]
        public async Task<ActionResult<List<CAttendList>>> Get()
        {
            var attendList = await _context.OrderDetail
                .Select(od => new CAttendList
                {
                    OrderDetailId = od.OrderDetailId,
                    Name = od.Member.Name,
                    Email = od.Member.Email,
                    odNumber = $"OD{od.Order.OrderTime.Date.ToString("yyyyMMdd")}{od.OrderDetailId.ToString("0000")}",
                    IsAttend = od.IsAttend,
                }).ToListAsync();

            return attendList;
        }

        // GET api/<AttendController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CAttendList>> Get(int id)
        {
            CAttendList attend = await _context.OrderDetail.Where(o => o.OrderDetailId == id)
                .Select(o => new CAttendList
            {
                    OrderDetailId = o.OrderDetailId,
                    Name = o.Member.Name,
                    Email = o.Member.Email,
                    odNumber = $"OD{o.Order.OrderTime.Date.ToString("yyyyMMdd")}{o.OrderDetailId.ToString("0000")}",
                    IsAttend = o.IsAttend,
                }).FirstAsync();

            return attend;
        }

        // POST api/<AttendController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AttendController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttend(int id,CAttendList attend)
        {
            if (id != attend.OrderDetailId)
            {
                return BadRequest();
            }

            OrderDetail od = await _context.OrderDetail.FindAsync(id);
            od.IsAttend = true;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<AttendController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/Attend/ScanQrCode?productDetailId=1&qrcode=OD202007080006
        [HttpGet]
        [Route("ScanQrCode")]
        public IActionResult scanQrCode(int productDetailId, string qrcode)
        {
            if (qrcode == null)
                return Content("請重掃");

            if (qrcode.Length != 14)
                return Content("票號格式錯誤");

            int year = Convert.ToInt32(qrcode.Substring(2, 4));
            int month = Convert.ToInt32(qrcode.Substring(6, 2));
            int day = Convert.ToInt32(qrcode.Substring(8, 2));
            DateTime date = new DateTime(year, month, day);

            int id = Convert.ToInt32(qrcode.Substring(10, 4).TrimStart('0'));

            var od = _context.OrderDetail
                .Include(o => o.Order)
                .Include(o => o.Member)
                .Where(x => x.OrderDetailId == id && x.Order.OrderTime.Date == date.Date).FirstOrDefault();

            if (od != null)
            {
                if (od.IsAttend == null && od.ProductDetailId == productDetailId)
                {
                    od.IsAttend = true;
                    _context.SaveChanges();
                    return Content($"{od.Member.Name}，報到成功!");
                }
                else if (od.IsAttend != null && od.ProductDetailId == productDetailId)
                {
                    return Content("此票號已有報到記錄");
                }
                else
                {
                    return Content("票號不屬於此課程，請確認");
                }
            }
            else
            {
                return Content("查無此票號");
            }
        }
    }
}
