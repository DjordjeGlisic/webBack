using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlavniController:ControllerBase
    {
        public CetContext _context{get;set;}
        public GlavniController(CetContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("VratiUserName")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var korisnickaImena=_context.Korisnici;
                return Ok(
                    korisnickaImena.Select(p=>new{
                    Id=p.ID,
                    Username=p.Korisncko,
                    boja=p.Boja

                }
                
                ).ToList());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class KorisnikForma
        {
            public string Korisncko { get; set;}
            public string Nadimak{get;set;}
            
        }  
        [HttpPost]
        [Route("DodajKorisnikaSobi/{Soba}")]
        public async Task<ActionResult> DodajKorisnikaSobi([FromRoute]string Soba,[FromBody]KorisnikForma korisnik)
        {
            var soba=_context.Sobe.Include(p=>p.KorisniciSaNadimkom).Where(p=>p.Naziv==Soba).FirstOrDefault();
            var kor=_context.Korisnici.Include(p=>p.KorisniciSaNadimkom).Where(p=>p.Korisncko==korisnik.Korisncko).FirstOrDefault();
            if(kor==null)
            {
                return BadRequest("Nije pronadjen");
            }
            if(soba==null)
            {
                var s=new Soba{
                    Naziv=Soba,
                    MaxBrojClanova=5
                };
                var cet=new Cet{
                    Nadimak=korisnik.Nadimak,
                    Korisnik=kor,
                    Soba=s
                };
                await _context.Cetovi.AddAsync(cet);
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodata soba korisniku");
            }
            if(soba.MaxBrojClanova<soba.KorisniciSaNadimkom.Count+1)
            {
                return BadRequest("Prevazidjen je kapacitet sobe");

            }
            var cet2=new Cet{
                Nadimak=korisnik.Nadimak,
                Korisnik=kor,
                Soba=soba
            };
                await _context.Cetovi.AddAsync(cet2);
            
            await _context.SaveChangesAsync();
            return Ok("Uspesno dodata soba korisniku");
        }
        [HttpPost]
        [Route("DodajKorisnika")]
        public async Task<ActionResult> DodajKorisnika([FromBody]Korisnik korisnik)
        {
           
            try
            {
            var kor=await _context.Korisnici.Where(p=>p.Korisncko==korisnik.Korisncko).FirstOrDefaultAsync();
            if(kor==null)
            {
                
                    if(string.IsNullOrWhiteSpace(korisnik.Korisncko)||string.IsNullOrWhiteSpace(korisnik.Ime)||string.IsNullOrWhiteSpace(korisnik.Prezime))
                    {
                        return BadRequest("Prazna polja");
                    }
                    if(korisnik.Korisncko.Length>15||korisnik.Ime.Length>25||korisnik.Prezime.Length>25)
                    {
                        return BadRequest("dugacko ime");
                    }
                    if(string.IsNullOrEmpty(korisnik.Email)||!korisnik.Email.Contains("@")||!korisnik.Email.Contains(".com")||korisnik.Email.Length>20)
                    {
                        return BadRequest("Los email");
                    }
                   
                     if(string.IsNullOrEmpty(korisnik.Sifra)||korisnik.Sifra.Length<8)
                    {
                        return BadRequest("Slaba sifra");
                    }
                  
                    await _context.AddAsync(korisnik);
                   
                    

                
                
            await _context.SaveChangesAsync();
            return Ok("Uspesno je dodat korisnik u sobu");
            }
            return BadRequest("Korisnicko ime vec postoji");            

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("PrebrojiJedinstevne/{IdSobe}")]
        public async Task<IActionResult> Get([FromRoute]int IdSobe)
        {
            try
            {
                var query=_context.Sobe.Include(p=>p.KorisniciSaNadimkom).AsQueryable();
                var q=_context.Cetovi.Include(p=>p.Korisnik).Include(p=>p.Soba).ToList();
               // var q=_context.Cetovi.AsQueryable();
                var soba=query.Where(p=>p.ID==IdSobe).FirstOrDefault();
                int count=0;
                foreach(var korisnikNadimak in soba.KorisniciSaNadimkom)
                {
                    var korisnik=korisnikNadimak.Korisnik;
                    var postoji=q.Where(p=>p.Soba.ID!=IdSobe&&p.Korisnik.ID==korisnik.ID).ToList();
                    if(postoji.Count==0)
                    {count++;}
                }
                return Ok(count);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiCetSobe")]
        public async Task<ActionResult> VratiCetSobe()
        {
            try
            {
                var sobe=_context.Sobe.Include(p=>p.KorisniciSaNadimkom).ThenInclude(k=>k.Korisnik);
                return Ok(sobe.Select(p=>new{
                    id=p.ID,
                    soba=p.Naziv,
                    clanovi=p.KorisniciSaNadimkom.Select(k => new{
                        nadimak=k.Nadimak,
                        korisnicko=k.Korisnik.Korisncko,
                        boja=k.Korisnik.Boja
                    }).ToList()
                }).ToList());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}