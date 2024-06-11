using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApiBatch9.Models;

namespace WebApiMorningOnlineBatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DatabaseContext _context;
        public ValuesController(DatabaseContext context)
        {
            _context = context;
        }

        //inserting data
        [HttpPost]
        [Route("PostingData")]
        public async Task<ActionResult<CurdModel>> Insert(CurdModel obj)
        {
            if (obj == null)
            {
                return NoContent();
            }
            else
            {
                _context.Add(obj);
                int x = await _context.SaveChangesAsync();
                if (x > 0)
                {
                    return Ok(new { meaage = "Inserted Succssfully" });
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpGet]
        [Route("Gets")]
        public async Task<ActionResult<IEnumerable<CurdModel>>> GetAlldata()
        {
            var res = (from s in _context.curdmodel select s).ToList();
            if (res == null)
            {
                return NoContent();
            }
            else
            {
                return res;
            }
        }

    }
}
