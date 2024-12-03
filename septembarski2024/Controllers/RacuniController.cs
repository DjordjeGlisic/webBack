using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RacuniController:ControllerBase
    {
        public ZadatakContext _context{get;set;}
        public RacuniController(ZadatakContext context)
        {
            _context = context; 
        }
        [HttpPost]
        [Route("DodajRacun/{BrStan}")]
        public async Task<ActionResult> DodajRacun([FromRoute]int BrStan,[FromBody]Racun racun)
        {
            if(BrStan<0||BrStan>100)
                return BadRequest("Ne validan broj stana");
            var stan=_context.Stanovi.Where(p=>p.BrojStana==BrStan).FirstOrDefault();
            if(stan==null)
                return BadRequest("Ne postoji stan sa tim brojem u bazi");
            if(racun.MesecIzdavanja<1||racun.MesecIzdavanja>12)
                return BadRequest("Niste uveli validan mesec");
            float Komunalije=stan.BrojClanova*100;
            float Struja=stan.BrojClanova*150;
            if(float.IsNaN(racun.Voda)||racun.Voda<0)
                return BadRequest("Niste uveli validan iznos za vodu");
            try
            {
                racun.brojStana=stan;
                racun.Struja=Struja;    
                racun.Komunalije=Komunalije;
                await _context.Racuni.AddAsync(racun);
                await _context.SaveChangesAsync();
                return Ok($"Uspesno dodat racun sa ID: {racun.ID} stanu sa brojem {stan.BrojStana}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);   
            }
        }
        

    }
}