using System;
using System.Linq;
using System.Net;
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
        public IznajmContext _context{get;set;}
        public GlavniController(IznajmContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("DodajAuto/{NazivModela}")]
        public async Task<ActionResult> DodajAuto([FromRoute] string NazivModela,[FromBody]Automobil automobil)
        {
            var model=_context.Modeli.Where(p=>p.NazivModela==NazivModela).FirstOrDefault();
            if(model==null)
                return BadRequest("Greska");
            try
            {
                if(automobil.BrojSedista<2||automobil.BrojSedista>6)
                    return BadRequest("Greska");
                 if(automobil.CenaPoDanu<1.0f||automobil.CenaPoDanu>1000.0f)
                    return BadRequest("Greska");
                 if(automobil.Kilometraza<1||automobil.Kilometraza>500000)
                    return BadRequest("Greska");
                 automobil.ModelAutomobila=model;
                 automobil.BrojDana=-1;
                 await _context.Automobili.AddAsync(automobil);
                 await _context.SaveChangesAsync();
                 return Ok($"Auto:{automobil.ID}");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        [HttpPost]
        [Route("DodajModel/{NazivModela}")]
        public async Task<ActionResult> DodajModel([FromRoute] string NazivModela)
        {
            var model=_context.Modeli.Where(p=>p.NazivModela==NazivModela).FirstOrDefault();
            if(model!=null)
                return BadRequest("Model vec postoji");
            try
            {
                Model mod=new Model
                {
                    NazivModela=NazivModela,
                };
                await _context.Modeli.AddAsync(mod);
                await _context.SaveChangesAsync();
                return Ok($"Auto:{mod.ID}");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
         [HttpPost]
        [Route("IznajmiAuto/{IdAuta}/{BrojDana}")]
        public async Task<ActionResult> DodajKorisnikaSaAutom([FromRoute] int IdAuta,[FromRoute]int BrojDana,[FromBody]Korisnik korisnik)
        {
            var auto=_context.Automobili.Where(p=>p.ID==IdAuta).Include(p=>p.KorisnikAutomobila).FirstOrDefault();
            if(auto==null)
                return BadRequest("Auto ne postoji");
            if(auto.KorisnikAutomobila!=null)
                return BadRequest("Auto je vec iznajmljeno");
            try
            {
              
               if(string.IsNullOrWhiteSpace(korisnik.Ime)|korisnik.Ime.Length>50)
                    return BadRequest("Greska lose ime");
                if(string.IsNullOrWhiteSpace(korisnik.Prezime)|korisnik.Prezime.Length>50)
                    return BadRequest("Greska lose prezime");
                 if(korisnik.Jmbg<1000000000000||korisnik.Jmbg>9999999999999)
                    return BadRequest("Greska los jmbg");
                if(korisnik.BrojVozacke<100000000||korisnik.BrojVozacke>999999999)
                    return BadRequest("Greska los broj vozacke");
                if(_context.Korisnici.Where(p=>p.Jmbg==korisnik.Jmbg).Any()==true&&_context.Korisnici.Where(p=>p.BrojVozacke==korisnik.BrojVozacke).Any()==true)
                {
                    var kor=_context.Korisnici.Where(p=>p.Jmbg==korisnik.Jmbg).Include(p=>p.TrenutnoIznajmljeni).FirstOrDefault();
                    if(kor.BrojVozacke!=korisnik.BrojVozacke)
                    {
                        return BadRequest("Korisnik sa datim jmbg nema taj broj vozacke");
                    }
                    auto.KorisnikAutomobila=kor;
                    auto.BrojDana=BrojDana;
                }
                else
                {
                    auto.KorisnikAutomobila=korisnik;
                    auto.BrojDana = BrojDana;
                    
                     await _context.Korisnici.AddAsync(korisnik);
                }
                 await _context.SaveChangesAsync();
                 return Ok($"Iznajmljeno je auto");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        [HttpGet]
        [Route("VratiModele")]
        public async Task<IActionResult> GetModels()
        {
            try
            {
                var modeli=_context.Modeli;
                return Ok(modeli.Select(p=>new{
                    Id=p.ID,
                    Naziv=p.NazivModela

                }).ToList());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("PretraziAuta/{model}/{cena}/{brojSedista}/{kilometraza}")]
        public async Task<ActionResult> FilterAuta([FromRoute] int model,[FromRoute]float cena,[FromRoute]int brojSedista,[FromRoute]float kilometraza)
        {
           
            try
            {
                var auta=_context.Automobili.Include(p=>p.ModelAutomobila).Include(p=>p.KorisnikAutomobila).AsQueryable();
                if(model!=-1)
                {
                    auta=auta.Where(p=>p.ModelAutomobila.ID==model);
                }
                if(cena!=-1)
                {
                    auta=auta.Where(p=>p.CenaPoDanu==cena);
                }
                if(brojSedista!=-1)
                {
                    auta=auta.Where(p=>p.BrojSedista==brojSedista);
                }
                if(kilometraza!=-1)
                {
                    auta=auta.Where(p=>p.Kilometraza==kilometraza);
                }
               
              var rezultati=await  auta.Select(p=>
                new
                {
                    Id=p.ID,
                    Model=p.ModelAutomobila.NazivModela,
                    kilometraza=p.Kilometraza,
                    Godiste=p.Godiste,
                    BrojSedista=p.BrojSedista,
                    CenaPoDanu=p.CenaPoDanu,
                    Iznajmljen=p.KorisnikAutomobila==null?"Ne":"Da"
                })
                .ToListAsync();
                
              
                return Ok(rezultati);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}