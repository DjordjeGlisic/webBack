using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;

namespace Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProizvodController:ControllerBase
    {
        public ProdavnicaContext _context{get;set;}
        public ProizvodController(ProdavnicaContext context)
        {
            _context=context;
        }
        [HttpPost]
        [Route("DodajProizvod")]
        public async Task<ActionResult> DodajProizvod(Proizvod proizvod)
        {
            if(proizvod.Cena<0||proizvod.Cena>999)
              return BadRequest("Cena nije validna");
            if(string.IsNullOrWhiteSpace(proizvod.Naziv)||proizvod.Naziv.Length>50)
              return BadRequest("Ime nije validno");
            if(proizvod.Kolicina<0||proizvod.Kolicina>100)
              return BadRequest("Kolicina nije validna");
            if(proizvod.Kolicina<0||proizvod.Kolicina>100)
              return BadRequest("Kolicina nije validna");
            
            try
            {
                await _context.Proizvodi.AddAsync(proizvod);
                await _context.SaveChangesAsync();
                return Ok($"Uspesno dodat proizvod sa ID={proizvod.Id}");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiProizvod/{ID}")]
        public async Task<ActionResult> vratiProizvod([FromRoute]int ID)
        {
            if(ID<0||ID>100)
                return BadRequest("Nevalidan id proizvoda");
            try
            {
                var proizvod=_context.Proizvodi.Where(p=>p.Id==ID).FirstOrDefault();
                if(proizvod==null)
                    return BadRequest("Ne postoji taj proizvod u bazi");
                return Ok(
                new{
                    Naziv=proizvod.Naziv,
                    Opis=proizvod.Opis,
                    PreostaloKomada=proizvod.Kolicina,
                    Cena=proizvod.Cena

                });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         [HttpPost]
        [Route("DodajKategoriju")]
        public async Task<ActionResult> Dodajkategoriju(Kategorija  kategorija)
        {
            if(string.IsNullOrWhiteSpace(kategorija.Naziv)||kategorija.Naziv.Length>50)
              return BadRequest("Ime nije validno");
            var postoji=_context.Kategorije.Where(p=>p.Naziv==kategorija.Naziv).FirstOrDefault();
            if(postoji!=null)
                return BadRequest("Kategorija sa datim imenom vec postoji u bazi");
            try
            {
                await _context.Kategorije.AddAsync(kategorija);
                await _context.SaveChangesAsync();
                return Ok($"Uspesno dodata kategorija za  proizvode sa ID={kategorija.Id}");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiSveKategorije")]
        public async Task<ActionResult> VratiKategorije()
        {
            try
            {   var lista= await _context.Kategorije.Select(p=>
                new 
                {
                    ID=p.Id,
                    Naziv=p.Naziv
                }).ToListAsync();
                return Ok(lista);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiProizvodeOdredjenogTipa/{Tip}")]
        public async Task<ActionResult> VratiProizvodeOdredjenogTipa([FromRoute]int Tip)
        {
            try
            {
                if(Tip<0||Tip>100)
                    return BadRequest("Nevalidan id tipa");
                var proizvodi=await _context.Proizvodi
                .Include(p=>p.Kategorija).Where(p=>p.Kategorija!=null&&p.Kategorija.Id==Tip).ToListAsync();
                return Ok(proizvodi.Select(p=>
                new{
                    Id=p.Id,
                    Naziv=p.Naziv,
                    Kolicina=p.Kolicina,
                    Cena=p.Cena,
                    Opis=p.Opis,
                    Tip=p.Kategorija.Naziv

                }));


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DodajKategorijuProizvodu/{IdKategorije}/{IdProizvoda}")]
        public async Task<ActionResult> DodajKategorijuProizvodu([FromRoute]int IdKategorije,[FromRoute]int IdProizvoda)
        {
            if(IdKategorije<0||IdKategorije>100)
                return BadRequest("Pogresan id kategorije");
            if(IdProizvoda<0||IdProizvoda>100)
                return BadRequest("Pogresan id proizvoda");
            var proizvod=await _context.Proizvodi.Where(p=>p.Id==IdProizvoda)
            .Include(p=>p.Kategorija).FirstOrDefaultAsync();
            var kategorija=await _context.Kategorije.Where(p=>p.Id==IdKategorije).FirstOrDefaultAsync();
            if(proizvod==null ||kategorija==null)
                return BadRequest("proizvod ili kategorija nije pronadjena");
            try
            {
                proizvod.Kategorija=kategorija;
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodata kategorija proizvodu");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DodajProizvodUKorpu/{IdProizvoda}/{IdKorpe}")]
        public async Task<ActionResult> DodajUKorpu([FromRoute]int IdProizvoda,[FromRoute]int IdKorpe)
        {
            
            var proizvod=_context.Proizvodi.Include(p=>p.Korpe).Where(p=>p.Id==IdProizvoda).FirstOrDefault();
            var korpa=_context.Korpe.Include(p=>p.Proizvodi).Where(p=>p.Id==IdKorpe).FirstOrDefault();
            if(korpa==null)
            {
                korpa=new Korpa();
                await _context.AddAsync(korpa);
                await _context.SaveChangesAsync();
                
            }
            
            try
            {
            korpa.Proizvodi.Add(proizvod);
            proizvod.Korpe.Add(korpa);
         
             await _context.SaveChangesAsync();
                int suma=0;
                foreach(var i in korpa.Proizvodi)
                {
                    suma+=i.Cena;
                }
                return Ok(new{
                    id=korpa.Id,
                    proizvodi=korpa.Proizvodi.Select(p=>new{naziv=p.Naziv,cena=p.Cena}),
                    ukupno=suma

                });
            
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("NapraviKorpu")]
        public async Task<IActionResult> Korpa()
        {
            try
            {
                var korpa=new Korpa();
                 _context.Korpe.Add(korpa);
                await _context.SaveChangesAsync();
                return Ok(korpa.Id);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiKorpu/{ID}")]
        public async Task<ActionResult> vratiKorpu([FromRoute]int ID)
        {
            if(ID<0||ID>100)
                return BadRequest("Nevalidan id korpe");
            try
            {
                var korpa=_context.Korpe.Include(p=>p.Proizvodi).Where(p=>p.Id==ID).FirstOrDefault();
                if(korpa==null)
                    return BadRequest("Ne postoji ta korpa u bazi");
                return Ok(
                new{
                    Id=korpa.Id,
                    Proizvodi=korpa.Proizvodi.Select(p=>new{
                        Naziv=p.Naziv,
                        Cena=p.Cena
                    })

                });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("ObrisiKorpuSmanjiKolicinuProizvoda/{ID}")]
        public async Task<ActionResult> BrisiKorpu([FromRoute]int ID)
        {
            try{
                var korpa=_context.Korpe.Include(p=>p.Proizvodi).Where(p=>p.Id==ID).FirstOrDefault();
            foreach(var proizvod in korpa.Proizvodi)
            {
                proizvod.Kolicina-=1;
                proizvod.Korpe.Remove(korpa);
            }
            korpa.Proizvodi.Clear();
            _context.Korpe.Remove(korpa);
            await _context.SaveChangesAsync();
            return Ok($"Uspesno obrisana korpa sa id {ID}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}