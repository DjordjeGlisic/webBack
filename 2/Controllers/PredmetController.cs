using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Models;
namespace Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class PredmetController:ControllerBase
    {
        public FakultetContext _context{get;set;}
        public PredmetController(FakultetContext context)
        {
            _context=context;
        }
        [HttpPost]
        [Route("DodajPredmet")]
        public async Task<ActionResult> DodajPredmetAsync([FromBody]Predmet predmet)
        {
            try
            {
                var predmetResponse = await _context.predmeti.AddAsync(predmet);
                await _context.SaveChangesAsync();  
                if(predmetResponse!=null)
                    return Ok("Uspesno Dodat Predmet");
                return BadRequest("Greska");
                


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         [HttpGet]
        [Route("PreuzmiPredmete")]
        public async Task<ActionResult> PreuzmiPredmetAsync()
        {
            try
            {
                var predmetResponse = _context.predmeti;
                await _context.SaveChangesAsync();  
                if(predmetResponse!=null)
                    return Ok(predmetResponse.Select(p=>
                    new{
                        ID=p.Id,
                        Naziv=p.Naziv
                    }).ToList());
                return BadRequest("Greska");
                


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
