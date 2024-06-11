using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBatch9.Models;

namespace WebApiMorningOnlineBatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurdController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public CurdController(DatabaseContext context)
        {
            _context = context;
        }


        //get all data from database table
        [HttpGet]
        [Route("GetAllData")]
        public async Task<ActionResult<IEnumerable<CurdModel>>> GetallDatafromdatabase()
        {
            return await _context.curdmodel.ToListAsync();
        }

        [HttpGet]
        [Route("databyID")]
        public async Task<ActionResult<CurdModel>> getDatabyID(int id)
        {
            var data = await _context.curdmodel.FindAsync(id);
            if (data != null)
            {
                return data;
            }
            else
            {
                return NotFound(
                    new { message="nodata"}
                    );
            }
        }

        [HttpGet]
        [Route("GetListByGender")]
        public async Task<ActionResult<IEnumerable<CurdModel>>> getGenderdata(string gender)
        {
            
            if (gender == null)
            {
                return NoContent();
            }
            else
            {
                var  obj = (from s in _context.curdmodel where s.gender == gender select s).ToList();

                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return NoContent();
                }
               
            }

        }

        [HttpPost]
        [Route("posts")]
        public async Task<ActionResult<CurdModel>> Inserting(CurdModel obj)
        {
            if (obj == null)
            {
                return NoContent(
                    
                    );
            }
            else
            {
                _context.curdmodel.Add(obj);
                await _context.SaveChangesAsync();
                //  return CreatedAtAction("GetallDatafromdatabase", "curd",obj);

                return Ok(
                    new { message = "Inserted Successfully" }
                    );
            }
        }

        [HttpPut]
        [Route("UpdatesData")]
        public async Task<IActionResult> PutData(int id , CurdModel obj)
        {
            if (id!=obj.Id)
            {
                return BadRequest();

            }
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeletesData")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var data = await _context.curdmodel.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.curdmodel.Remove(data);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
