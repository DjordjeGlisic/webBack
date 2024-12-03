using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoController:ControllerBase
    {
        public Context _context{get;set;}
        public AutoController(Context context)
        {
            _context = context; 
        }
        public class AutoDTO
        {
            
              public string Naziv{get;set;}
               public string Model{get;set;}
                public string Boja{get;set;}
                 public float Cena{get;set;}
                  public int Kolicina{get;set;}
        }
        [HttpPost]
        [Route("DodajAuto/{IdBrenda}/{IDProdavnica}")]
        public async Task<ActionResult> DodajAuto([FromRoute]int IdBrenda,[FromRoute]int IDProdavnica,[FromBody]AutoDTO a)
        {
            try
            {
                 var brend=_context.Brendovi.Where(p=>p.Id==IdBrenda).FirstOrDefault();
                  var prodavnica=_context.Prodavnice.Where(p=>p.Id==IDProdavnica).FirstOrDefault();
                  if(prodavnica==null)
                    throw new Exception("Ne postoji prodavnica datog ID-a");
                 var auto=new Auto{
                        Naziv=a.Naziv,
                        Model=a.Model,
                        Boja=a.Boja,
                        Cena=a.Cena,
                        Kolicina=a.Kolicina,
                        BrendAuta=brend,
                        DatumPoslednjeProdaje=null,
                        Prodavnica=prodavnica
                        
                 };
               
                await _context.AddAsync(auto);
                await _context.SaveChangesAsync();
                return Ok(auto.Id);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPut]
        [Route("ProdajAuto/{ID}")]
        public async Task<ActionResult> ProdajAuto([FromRoute]int ID)
        {
            try
            {
                 var auto=_context.Auta.Where(p=>p.Id==ID).FirstOrDefault();
                 
                 if(auto==null)
                    throw new Exception("Ne postoji auto datog ID-a");
                if(auto.Kolicina-1==0)
                {
                    auto.BrendAuta=null;
                    auto.Prodavnica=null;
                    _context.Auta.Remove(auto);
                    return Ok(-1);
                }
                auto.Kolicina-=1;
                auto.DatumPoslednjeProdaje=new DateTime();
                auto.DatumPoslednjeProdaje=DateTime.Now; 
                await _context.SaveChangesAsync();
                var kol=auto.Kolicina;
                var dat=auto.DatumPoslednjeProdaje;

                
                return Ok(new {
                    Kolicina=auto.Kolicina,
                    Datum=auto.DatumPoslednjeProdaje
                });


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }

}