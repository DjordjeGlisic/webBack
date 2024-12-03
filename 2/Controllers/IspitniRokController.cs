using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class IspitniRokController:ControllerBase
    {
        public FakultetContext _context{get;set;}
        public IspitniRokController(FakultetContext context)
        {
            _context=context;
        }
        [HttpPost]
        [Route("DodajRok")]
        public async Task<ActionResult> DodajRokAsync([FromBody]IspitniRok rok)
        {
            try
            {
                var rokResponse = await _context.rokovi.AddAsync(rok);
                await _context.SaveChangesAsync();  
                if(rokResponse!=null)
                    return Ok($"Uspesno Dodat rok sa ID:{rok.Id}");
                return BadRequest("Greska");
                


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiRokove")]
        public async Task<ActionResult> vratiRokove()
        {
            var rokovi= _context.rokovi;
            return Ok(
                rokovi.Select(p=>
                new
                 {
                    ID=p.Id,
                    Rok=p.NazivRoka

                })
            );
        }
        [HttpPost]
        [Route("DodajOcenuStudentu/{Indeks}/{Rok}/{Predmet}/{ocena}")]
        public async Task<ActionResult> DodajOcenuStudentu([FromRoute]long Indeks,[FromRoute]int Rok,[FromRoute] int Predmet,[FromRoute]int ocena)
        {
            try
            {
                var student=_context.steudenti.Where(s=>s.Indeks==Indeks).FirstOrDefault();
                var predmet=_context.predmeti.Where(p=>p.Id==Predmet).FirstOrDefault();
                var rok=_context.rokovi.Where(r=>r.Id==Rok).FirstOrDefault();
                if(student==null||predmet==null||rok==null||ocena<6||ocena>10)
                    return BadRequest("Nije pronadjen student ili predmet ili rok ili ocena nije validna");
                var studpred = _context.stdprd
                                .Include(p => p.Predmet)
                                .Include(p => p.Student);
                var polozio = studpred.Where(p =>p.Student.Indeks==Indeks&&p.Predmet.Id == Predmet).FirstOrDefault();
                if (polozio != null)
                {
                    return BadRequest($"Student je vec polozio dati predmet u roku {polozio.Rok.NazivRoka}");
                }
                await _context.stdprd.AddAsync(new StudentPredmet{
                    Student=student,
                   Predmet=predmet,
                   Rok=rok,
                   Ocena=ocena 
                });
                await _context.SaveChangesAsync();
                

                return Ok(
                    studpred.Where(p=>p.Student.Indeks==Indeks).Select(q=>
                    new{
                        Id=q.Id,
                        Indeks=q.Student.Indeks,
                        Ime=q.Student.Ime,
                        Prezime=q.Student.Prezme,
                        Predmet=q.Predmet.Naziv,
                        Rok=q.Rok.NazivRoka,
                        Ocena=q.Ocena

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
