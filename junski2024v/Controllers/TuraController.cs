using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TuraController:ControllerBase
    {
        public Context _context{get;set;}
        public TuraController(Context context)
        {
            _context=context;
        }
        
        [HttpPost]
        [Route("DodajTuru")]
        public async Task<ActionResult> DodajTur()
        {
            try
            {
                var tura =new Tura{
                    Cena=0.0f,
                    PresotaloMesta=5
                };
                
                await _context.Ture.AddAsync(tura);
                await _context.SaveChangesAsync();
                return Ok(tura.Id);

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        [Route("VratiTure")]
        public async Task<ActionResult> VratiTur()
        {
            try
            {
                var tur=_context.Ture.Include(p=>p.Znamenitosti);

               
                return Ok(tur.Select(p=>
                new
                {
                    Id=p.Id,
                    PreostaloMesta=p.PresotaloMesta,
                    Cena=p.Cena,
                    Znamenitosti=p.Znamenitosti.Select(q=>
                    new
                    {
                        Id=q.Id,
                        Ime=q.ImeZnamenitosti
                    }
                    ).ToList()

                }
                ).ToList());

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        [Route("VratiTury/{ID}")]
        public async Task<ActionResult> VratiTuru([FromRoute]int ID)
        {
            try
            {
                var tur=_context.Ture.Include(p=>p.Znamenitosti).Where(p=>p.Id==ID).FirstOrDefault();

               
                return Ok(
                new
                {
                    Id=tur.Id,
                    PreostaloMesta=tur.PresotaloMesta,
                    Cena=tur.Cena,
                    Znamenitosti=tur.Znamenitosti.Select(q=>
                    new
                    {
                        Id=q.Id,
                        Ime=q.ImeZnamenitosti
                    }
                    ).ToList()

                }
                );

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }
         [HttpPost]
        [Route("FiltrirajTure/{ListaZnamenitosti}")]
        public async Task<ActionResult> FilterTur([FromRoute]string ListaZnamenitosti,[FromBody]Korisnik k)
        {
            try
            {
                bool menjajBoju=false;
                int? kreiranaNova=null;
                int? idStare=null;
                int? idNove=null;
                var znamIds=ListaZnamenitosti.Split("a")
                .Where(p=>int.TryParse(p,out _))
                .Select(int.Parse)
                .ToList();
                var sve=_context.Znamenitosti;
                var izabrane=new List<Znamenitost>();
                foreach(var znam in znamIds)
                {
                    var tr=sve.Where(p=>p.Id==znam).FirstOrDefault();
                    if(tr!=null)
                    {
                        izabrane.Add(tr);
                    }
                }
               
                if(izabrane.Count==0)
                    throw new Exception("Nije nadjena nijedna znamenitost");
                var ture=_context.Ture.Include(p=>p.Znamenitosti).ToList();
                bool nadjena=false;
                var korisnik=new Korisnik{
                            Ime=k.Ime,
                            Prezime=k.Prezime,
                            BrojLicneKarte=k.BrojLicneKarte

                        };
                foreach(var tura in ture)
                {
                    
                    bool imaSve=true;
                    foreach(var izabrana in izabrane)
                    {
                        if(tura.Znamenitosti.Contains(izabrana)==true)
                        {
                            continue;
                        }
                        else{
                            imaSve=false;
                            break;

                        }
                    
                    }
                    if(imaSve==true&&tura.PresotaloMesta>0)
                    {
                        
                        korisnik.Rezervacije.Add(tura);
                        tura.Rezervisani.Add(korisnik);
                        if(tura.PresotaloMesta-1==0)
                        {
                            menjajBoju=true;
                        }
                        tura.PresotaloMesta-=1;
                        idStare=tura.Id;
                    
                        nadjena=true;
                        await _context.SaveChangesAsync();
                        break;

                    }
                    
                    
                }
                if(nadjena==false)
                {
                    var tura=new Tura{
                        PresotaloMesta=4,
                        Cena=0.0f
                    };
                    foreach(var znam in izabrane)
                    {
                      tura.Cena+=znam.Cena*0.9f;
                      tura.Znamenitosti.Add(znam);
                      znam.PripadaTurama.Add(tura);
                    } 
                    tura.Rezervisani.Add(korisnik);
                    korisnik.Rezervacije.Add(tura); 
                    await _context.SaveChangesAsync();
                    idNove=tura.Id;
                        
                    

                }
               
                
                return Ok(new
                {
                    IdStare=idStare,
                    IdNove=idNove,
                    MenjajBoju=menjajBoju
                });

            }
            catch(Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }


    }
}