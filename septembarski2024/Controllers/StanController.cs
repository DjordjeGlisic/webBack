using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StanController:ControllerBase
    {
        public ZadatakContext _context;
        public StanController(ZadatakContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("BrojeviStana")]
        public async Task<ActionResult> vratiBrojeveStana()
        {
            var stanovi=_context.Stanovi;
            var odaberi=stanovi.Select(p=>
            new
            {
              Broj=p.BrojStana
            }).ToList();
            return Ok(odaberi);
        }
        [HttpGet]
        [Route("VratiStan/{Br}")]
        public async Task<ActionResult> VratiStan([FromRoute]int Br)
        {
            if(Br<0||Br>100)
                return BadRequest("Los broj");
            try
            {
                var stan=_context.Stanovi.Where(p=>p.BrojStana==Br).FirstOrDefault();
                               
                return Ok(new{
                    brojStana=stan.BrojStana,
                    imeVlasnika=stan.ImeVlasnika,
                    povrsina=stan.Povrsina,
                    brojClanova=stan.BrojClanova
                });
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpGet]
        [Route("VratiRacune/{Br}")]
        public async Task<ActionResult> VratiRacune([FromRoute]int Br)
        {
            if(Br<0||Br>100)
                return BadRequest("Los broj");
            try
            {
                var stan=_context.Stanovi.Include(p=>p.Racuni).Where(p=>p.BrojStana==Br).FirstOrDefault();
                               
                return Ok(stan.Racuni.Select(p=>
                new{
                    mesec=p.MesecIzdavanja,
                    voda=p.Voda,
                    struja=p.Struja,
                    komunalneUsluge=p.Komunalije,
                    placen=p.Placen
                }).ToList());
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpPost]
        [Route("DodajStan")]
        public async Task<ActionResult> DodajStan([FromBody] Stan stan)
        {
 
            
            
            if(stan.Povrsina<0||stan.Povrsina>100)
                return BadRequest("Nevalidna povrsina stana");
                try
                {
                    await _context.Stanovi.AddAsync(stan);
                    await _context.SaveChangesAsync();
                    return Ok($"Uspesno dodat stan sa brojem {stan.BrojStana}");

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            
        
        }
        [HttpPut]
        [Route("AzurirajStan")]
        public async Task<ActionResult> AzurirajStan([FromBody] Stan stan)
        {
            if(stan.BrojClanova<0||stan.BrojClanova>15)
                return BadRequest("Nemoguc broj clanova u stanu");
            
            if(stan.BrojStana<0||stan.BrojStana>1000)
                return BadRequest("Nevalidan broj stana");
            if(stan.Povrsina<0||stan.Povrsina>100)
                return BadRequest("Nevalidna povrsina stana");
                try
                {
                    var s = _context.Stanovi.Where(p=>p.BrojStana==stan.BrojStana).FirstOrDefault();
                   if(s==null)
                        return BadRequest("Uneli ste stan sa nepostojecim brojem"); 
                    s.BrojClanova = stan.BrojStana; 
                    s.Povrsina=stan.Povrsina;
                    s.ImeVlasnika=stan.ImeVlasnika;
                    

                    await _context.SaveChangesAsync();
                    return Ok("Uspesno azuriran stan");

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            
        
        }
        [HttpDelete]
        [Route("ObrisiStan/{Broj}")]
        public async Task<ActionResult> ObrisiStan([FromRoute] int Broj)
        {
            
            if(Broj<0||Broj>1000)
                return BadRequest("Nevalidan broj stana");
            try
                {
                    var s = _context.Stanovi.Where(p=>p.BrojStana==Broj).FirstOrDefault();
                   if(s==null)
                        return BadRequest("Uneli ste stan sa nepostojecim brojem"); 
                    _context.Stanovi.Remove(s);
                    await _context.SaveChangesAsync();
                    return Ok("Uspesno obrisan stan");

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            
        
        }
        [HttpGet]
        [Route("UkupnoZaduzenje/{BrojStana}")]
        public async Task<ActionResult> UkupnoZaduzenje([FromRoute] int BrojStana)
        {
            if(BrojStana<0||BrojStana>100)
                return BadRequest("Pogresan broj stana");
            var stan=_context.Stanovi.Where(p=>p.BrojStana==BrojStana).Include(p=>p.Racuni).FirstOrDefault();
            if(stan==null)
                return BadRequest("Ne postoji stan sa datim brojem u bazi");
            if(stan.Racuni.Count==0)
                return Ok(0);
            float sum=0.0f;
            foreach( var racun in stan.Racuni)
            {
                if(racun.Placen=="da"||racun.Placen=="Da"||racun.Placen=="DA")
                    continue;
                sum+=racun.Struja;
                sum+=racun.Voda;
                sum+=racun.Komunalije;
                
                
            }
            return Ok(sum);


        }

    }
}