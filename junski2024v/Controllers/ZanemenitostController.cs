using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZnamenitostController:ControllerBase
    {
        public Context _context{get;set;}
        public ZnamenitostController(Context context)
        {
            _context=context;
        }
        [HttpPost]
        [Route("DodajZnamenitost")]
        public async Task<ActionResult> DodajZ([FromBody]Znamenitost z)
        {
            try
            {
                await _context.Znamenitosti.AddAsync(z);
                await _context.SaveChangesAsync();
                return Ok(z);

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }
        [HttpPost]
        [Route("DodajZnamenitostTuri/{IdZnamenitosti}/{IdTure}")]
        public async Task<ActionResult> DodajZTur([FromRoute]int IdZnamenitosti,[FromRoute]int IdTure)
        {
            try
            {
                var tur=_context.Ture.Include(p=>p.Znamenitosti).Where(p=>p.Id==IdTure).FirstOrDefault();
                if(tur==null)
                    throw new Exception("Nije pronadjena tura");
                var z=_context.Znamenitosti.Include(p=>p.PripadaTurama).Where(p=>p.Id==IdZnamenitosti).FirstOrDefault();
                if(z==null)
                    throw new Exception("Nije pronadjena znamenistost");
                    
                 z.PripadaTurama.Add(tur);
                 tur.Znamenitosti.Add(z);
                 tur.Cena+=z.Cena*0.9f;
                 tur.PresotaloMesta-=1;
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodavanje");

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }
         [HttpGet]
        [Route("VratiZnamenitosti")]
        public async Task<ActionResult> VratiZnamenitosti()
        {
            try
            {
                var znam=_context.Znamenitosti;

               
                return Ok(znam.Select(p=>
                new
                {
                    
                        Id=p.Id,
                        Ime=p.ImeZnamenitosti
                    
                }
                ).ToList());

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }


    }
}