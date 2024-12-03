using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrendController:ControllerBase
    {
        public Context _context{get;set;}
        public BrendController(Context context)
        {
            _context = context; 
        }
        [HttpPost]
        [Route("DodajBrend")]
        public async Task<ActionResult> DodajBrend([FromBody]Brend b)
        {
            try
            {
                await _context.Brendovi.AddAsync(b);
                await _context.SaveChangesAsync();
                return Ok(b.Id);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiBrendove")]
        public async Task<ActionResult> VratiBrendove()
        {
            try
            {
              var brendovi=_context.Brendovi;
                return Ok(
                    brendovi.Select(p=>
                    new{
                        Id=p.Id,
                        Naziv=p.Naziv
                    
                }).ToList()
                );


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}