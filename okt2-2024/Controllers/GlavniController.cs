using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlavniController:ControllerBase
    {
        public PortalContext _context{get;set;}
        public GlavniController(PortalContext cntext)
        {
            _context = cntext;
        }
        [HttpGet]
        [Route("VratiSastojke")]
        public async Task<ActionResult> GetSastojci()
        {
            try
            {
                var sastoji=_context.Sastojci;
                return Ok(sastoji.Select(p=>new{
                    Id=p.Id,
                    Naziv=p.Naziv

                }).ToList());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("DodajSastojak/{Naziv}/{Boja}")]
        public async Task<ActionResult> DodajSastojak([FromRoute] string Naziv,[FromRoute] string Boja)
        {
             try
            {
                var postoji=_context.Sastojci.Where(p=>p.Naziv==Naziv).FirstOrDefault();
                if(postoji!=null)
                    throw new Exception("Sastojak sa imenom vec postoji");
                var sastojak=new Sastojak{
                    Naziv=Naziv,
                    Boja=Boja
                };
                await _context.Sastojci.AddAsync(sastojak);
                await _context.SaveChangesAsync();
                return Ok(new{

                    Id=sastojak.Id,
                    Naziv=sastojak.Naziv
                }

                );

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         [HttpPost]
        [Route("DodajRecept")]
        public async Task<ActionResult> DodajRecept([FromBody]Recept r)
        {
             try
            {
                await _context.Recepti.AddAsync(r);
                await _context.SaveChangesAsync();
                return Ok(r.Id

                );

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         [HttpPost]
        [Route("DodajSastojakReceptu/{Sastojak}/{Recept}/{Kolicina}")]
        public async Task<ActionResult> DodajSastojakReceptu([FromRoute]string Sastojak,[FromRoute] string Recept,[FromRoute]string Kolicina)
        {
             try
            {
                var sastojak=_context.Sastojci.Where(p=>p.Naziv==Sastojak).FirstOrDefault();
                var recept=_context.Recepti.Where(p=>p.Ime==Recept).FirstOrDefault();
                var veza=new SastojakRecept{
                    Sastojak=sastojak,
                    Recept=recept,
                    Kolicina=Kolicina


                };

                await _context.SastojakRecepts.AddAsync(veza);
                await _context.SaveChangesAsync();
                return Ok(veza.Id

                );

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("Filter/{sastojci}")]
        public async Task<ActionResult> FilterSastojci([FromRoute]string sastojci)
        {
            try
            {
                var nizSastojaka=sastojci.Split(",").ToList();
                List<Recept> filter=new List<Recept>();
                foreach(var ime in nizSastojaka)
                {
                    var s=_context.SastojakRecepts.Include(p=>p.Sastojak).Include(p=>p.Recept).ThenInclude(p=>p.listaSastojka).ThenInclude(p=>p.Sastojak).Where(p=>p.Sastojak.Naziv==ime).FirstOrDefault();
                    if(s!=null&&filter.Contains(s.Recept)==false)
                        filter.Add(s.Recept);
                }
                if(filter.Count()==0)
                {
                    throw new Exception("Nije pronadjen nijedan uneti sastojak u nekom receptu");
                }
                List<Recept>rez=new List<Recept>();   
                foreach(var recept in  filter)
                {
                    bool posedujeSveSastojke=true;
                    foreach(var unetSastojak in nizSastojaka)
                    {
                        var unet=_context.SastojakRecepts.Where(p=>p.Sastojak.Naziv==unetSastojak&&p.Recept==recept).FirstOrDefault();
                        if(unet!=null)
                            continue;
                        else
                        {
                            posedujeSveSastojke=false;
                            break;
                        }
                        
                    }
                    if(posedujeSveSastojke==true)
                    {
                        rez.Add(recept);
                    }
                    
                }
               
               
                return Ok(rez.Select(p=>new {
                    Id=p.Id,
                    Ime=p.Ime,
                    Opis=p.TextRecepta,
                    Sastojci=p.listaSastojka.Select(q=>new{
                        Id=q.Sastojak.Id,
                        Naziv=q.Sastojak.Naziv,
                        Boja=q.Sastojak.Boja
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