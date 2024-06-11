using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApiBatch9.Models;
using Microsoft.EntityFrameworkCore;

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

        // update login and delete

        [HttpGet]
        [Route("getDatabyID")]
        public async Task<ActionResult<CurdModel>> getDataid(int id)
        {
            var data = await _context.curdmodel.FindAsync(id);
            if (data != null)
            {
                return data;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Route("UpdateData")]
        public async Task<ActionResult<CurdModel>> UpdatesData(CurdModel obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(obj).State = EntityState.Modified;
                int x = await _context.SaveChangesAsync();
                if (x > 0)
                {
                    return Ok(new { message = "Updated successfully" });
                }
                else
                {
                    return NoContent();
                }
            }
        }

        [HttpDelete]
        [Route("DeleteData")]
        public async Task<ActionResult<CurdModel>> Delete(int id)
        {
            var res = await _context.curdmodel.FindAsync(id);
            if (res != null)
            {
                _context.Remove(res);
                int x = await _context.SaveChangesAsync();
                if (x > 0)
                {
                    return Ok(new { message = "Deleted Successfully" });
                }
                else
                {
                    return BadRequest();
                }
            }

            return NoContent();

        }

    }
}
