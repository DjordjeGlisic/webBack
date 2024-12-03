using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdavnicaController:ControllerBase
    {
        public Context _context{get;set;}
        public ProdavnicaController(Context context)
        {
            _context = context; 
        }
        public class ProdavnicaDTO
        {
                 public string Naziv{get;set;}
                  public string Lokacija{get;set;}

        }
        [HttpPost]
        [Route("DodajProdavnicu")]
        public async Task<ActionResult> DodajProdavnicu([FromBody]ProdavnicaDTO p)
        {
            try
            {
                var prodavnica=new Prodavnica{
                    Naziv=p.Naziv,
                    Lokacija=p.Lokacija
                };
                await _context.Prodavnice.AddAsync(prodavnica);
                await _context.SaveChangesAsync();
                return Ok(prodavnica.Id);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiBrendoveAutaUProdavnici/{ID}")]
        public async Task<ActionResult> VratiProdavnicuBrendovi([FromRoute]int ID)
        {
            try
            {
                var prodavnica=_context.Prodavnice.Include(p=>p.Proizvodi).ThenInclude(p=>p.BrendAuta).Where(p=>p.Id==ID).FirstOrDefault();
                HashSet<int> IdBrendovaProdavnice=new HashSet<int>();
               
                foreach(var auto in prodavnica.Proizvodi)
                {
                    IdBrendovaProdavnice.Add(auto.BrendAuta.Id);

                }
                var ListaBrendova=new List<Brend>();
                foreach(var id in IdBrendovaProdavnice)
                {
                    var brend=_context.Brendovi.Where(p=>p.Id==id).FirstOrDefault();
                    ListaBrendova.Add(brend);
                }
                return Ok(ListaBrendova.Select(p=>new{
                    ID=p.Id,
                    Naziv=p.Naziv
                }).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiBojeAutaUProdavnici/{ID}")]
        public async Task<ActionResult> VratiProdavnicuBoje([FromRoute]int ID)
        {
            try
            {
                var prodavnica=_context.Prodavnice.Include(p=>p.Proizvodi).Where(p=>p.Id==ID).FirstOrDefault();
                HashSet<string> SkupBoja=new HashSet<string>();
                SkupBoja.Add("");
               
                foreach(var auto in prodavnica.Proizvodi)
                {
                    SkupBoja.Add(auto.Boja);

                }
               
                return Ok(SkupBoja.Select(p=>new{
                    Boja=p
                }).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          [HttpGet]
        [Route("VratiModeleAutaUProdavnici/{ID}")]
        public async Task<ActionResult> VratiProdavnicuModeli([FromRoute]int ID)
        {
            try
            {
                var prodavnica=_context.Prodavnice.Include(p=>p.Proizvodi).Where(p=>p.Id==ID).FirstOrDefault();
                HashSet<string> SkupModela=new HashSet<string>();
                SkupModela.Add("");
                foreach(var auto in prodavnica.Proizvodi)
                {
                    SkupModela.Add(auto.Model);

                }
               
                return Ok(SkupModela.Select(p=>new{
                    Model=p
                }).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class Parametri
        {
          public  int Brend{get;set;}
           public string? Model{get;set;}
          public  string? Boja{get;set;}
        }
         [HttpPost]
        [Route("UcitajAuta/{ID}")]
        public async Task<ActionResult> UcitajAuta([FromRoute]int ID,[FromBody] Parametri parametri)
        {
            try
            {
                var prodavnica=_context.Prodavnice.Include(p=>p.Proizvodi).ThenInclude(p=>p.BrendAuta).Where(p=>p.Id==ID).FirstOrDefault();
                var autaProdavnice=prodavnica.Proizvodi.Select(p=>new{
                    auto=p
                }).AsQueryable();
                if(prodavnica==null)
                    throw new Exception("Nije pronadjena prodavnica");
                 autaProdavnice=autaProdavnice.Where(p=>p.auto.BrendAuta.Id==parametri.Brend);
                if(parametri.Model!=null)
                    autaProdavnice=autaProdavnice.Where(p=>p.auto.Model==parametri.Model);
                
                if(parametri.Boja!=null)
                    autaProdavnice=autaProdavnice.Where(p=>p.auto.Boja==parametri.Boja);
                return Ok(
                    autaProdavnice.Select(p=>new{
                        Id=p.auto.Id,
                        Brend=p.auto.BrendAuta.Naziv,
                        Model=p.auto.Model,
                        Kolicina=p.auto.Kolicina,
                        DatumPoslednjeProdaje=p.auto.DatumPoslednjeProdaje,
                        Cena=p.auto.Cena*122

                    }).ToList()
                );
                
                 
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}