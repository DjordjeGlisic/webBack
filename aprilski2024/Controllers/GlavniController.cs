using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlavniController : ControllerBase
    {
        public BioskopContext _context { get; set; }
        public GlavniController(BioskopContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("DodajSalu")]
        public async Task<ActionResult> DodajSalu([FromBody] Sala sala)
        {
            if (sala.Redovi < 0 || sala.Redovi > 1000)
                return BadRequest("Greska");
            if (sala.Sedista < 0 || sala.Sedista > 1000)
                return BadRequest("Greska");
            try
            {
                await _context.Sale.AddAsync(sala);
                await _context.SaveChangesAsync();
                return Ok(sala.ID);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class ProjjekcijaDTO
        {
            public string Naziv { get; set; }
            public int Godina { get; set; }
            public int Mesec { get; set; }
            public int Dan { get; set; }
            public int Sati { get; set; }
            public int Mintui { get; set; }




            public long Sifra { get; set; }
        }
        [HttpPost]
        [Route("DodajProjekciju/{IdSale}")]
        public async Task<ActionResult> DodajProjekciju([FromRoute] int IdSale, [FromBody] ProjjekcijaDTO dto)
        {
          
            if(string.IsNullOrWhiteSpace(dto.Naziv))
                return BadRequest("Greska");
            var sala=_context.Sale.Where(p=>p.ID==IdSale).FirstOrDefault();
            if(sala==null)
                return BadRequest("Greska");
            var postoji=_context.Projekcije.Where(p=>p.Sifra==dto.Sifra).FirstOrDefault();
            if(postoji!=null)
                return BadRequest("Greska");
            if (dto.Godina < 2024 || dto.Godina > 2025 || dto.Mesec < 1 || dto.Mesec > 12 || dto.Dan < 1 || dto.Dan > 31 || dto.Sati < 0 || dto.Sati > 60 || dto.Mintui < 0 || dto.Mintui > 60)
                return BadRequest("Pogresan datum");
            DateTime datum = new DateTime(dto.Godina,dto.Mesec,dto.Dan,dto.Sati,dto.Mintui,0);
            if(datum<DateTime.Now.Date)
                return BadRequest("Uneliste datum u proslosti");

            try
            {
                Projekcija projekcija = new Projekcija
                {
                    Naziv = dto.Naziv,
                    Datum = datum,
                    Sifra = dto.Sifra
                };
                projekcija.Lokacija=sala;
                for(int i=0;i<sala.Redovi;i++)
                {
                    for(int j=0;j<sala.Sedista;j++)
                    {
                        Karta karta=new Karta
                        {
                            Red=i,
                            Sediste=j,
                            Cena=2000.0f-(2000.0f*0.03f*i),
                            Kupljena=false


                        };
                        karta.PripadaProjekciji=projekcija;
                        await _context.AddAsync(karta); 
                    }
                }
                await _context.Projekcije.AddAsync(projekcija);
                await _context.SaveChangesAsync();
                return Ok(projekcija.ID);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         [HttpGet]
        [Route("VratiProjekcije")]
        public async Task<IActionResult> GetProjections()
        {
            var projekcije=_context.Projekcije.Include(p=>p.Lokacija);
        
            try
            {
                return Ok(projekcije.Select(p=>
                new{
                    ID=p.ID,
                    Naziv=p.Naziv,
                    Datum=p.Datum,
                    Sala=p.Lokacija.Naziv,
                    IdSale=p.Lokacija.ID

                }).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiKarte/{IdSale}/{IdProjekcije}")]
        public async Task<IActionResult> Get([FromRoute]int IdSale,[FromRoute]int IdProjekcije)
        {
            var projekcije=_context.Projekcije.Include(p=>p.Lokacija).Include(P=>P.Karte).Where(p=>p.ID==IdProjekcije).AsQueryable();
            if(projekcije==null)
                return BadRequest("Nismo pronasli projekciju sa datim imenom");
            var projekcijaUsali=projekcije.Where(p=>p.Lokacija.ID==IdSale).FirstOrDefault();
            if(projekcijaUsali==null)
                return BadRequest($"Projekcija {IdProjekcije} se ne odrzava u datoj sali");
            try
            {
                return Ok(projekcijaUsali.Karte.Select(p=>
                new{
                    ID=p.ID,
                    Red=p.Red,
                    Broj=p.Sediste,
                    Rezervisana=p.Kupljena,
                    Cena=p.Cena,
                    Sifra=projekcijaUsali.Sifra

                }).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("PrikaziDetaljeKarte/{ID}")]
        public async Task<IActionResult> GetKarta([FromRoute]int ID)
        {
          
            try
            {
                var karta=_context.Karte.Where(p=>p.ID==ID).Include(p=>p.PripadaProjekciji).FirstOrDefault();
                if(karta==null)
                {
                    throw new Exception("Ne postoji");
                }
                return Ok(new{
                    Red=karta.Red,
                    BrojSedista=karta.Sediste,
                    CenaKarte=karta.Cena,
                    Sifra=karta.PripadaProjekciji.Sifra,
                    ID=karta.ID
                    

                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("KupiKartu/{ID}")]
        public async Task<ActionResult> KupiKartu([FromRoute]int ID)
        {
            try
            {
                var karta= await _context.Karte.Where(p=>p.ID==ID).FirstOrDefaultAsync();
                if(karta==null)
                    throw new Exception("Nismo nasli kartu");
                karta.Kupljena=true;
                await _context.SaveChangesAsync();
                return Ok("Uspesno kupljena karta");


            }
            catch(Exception  ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}