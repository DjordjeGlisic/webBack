using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers{
    [ApiController]
    [Route("[controller]")]
    public class ProdavnicaController:ControllerBase
    {
        public ZadatakContext _context{get;set;}
        public ProdavnicaController(ZadatakContext context)
        {
            _context=context;
        }
        [HttpPost]
        [Route("DodajProdavnicu")]
        public async Task<ActionResult>DodajProdavnicu([FromBody]Prodavnica p)
        {
            try
            {
                
                if(string.IsNullOrEmpty(p.Naziv)||p.Naziv.Length>30)
                    throw new Exception("Niste uneli validan naziv");
                if(string.IsNullOrEmpty(p.Lokacija)||p.Lokacija.Length>30)
                    throw new Exception("Niste uneli validnu lokaciju");
                if(p.Telefon.ToString().Length!=9)
                    throw new Exception("Niste uneli validan telefon");
                
                await _context.Prodavnice.AddAsync(p);
                await _context.SaveChangesAsync();
                return Ok(p.Id);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiProizvodeProdavnice")]
        public async Task<ActionResult>VratiProizvodeProdavnice()
        {
            try
            {
                
                var prodavnice=_context.Prodavnice.Include(p=>p.ListaProizvoda);
                
                
                return Ok(
                    prodavnice.Select(q=>
                    new{
                        ID=q.Id,
                        Naziv=q.Naziv,                    
                        proizvodi=q.ListaProizvoda.Select(p=>
                        new
                        {
                            ID=p.Id,
                            Kolicina=p.Kolicina,
                            Naziv=p.Naziv
                        }
                        ).ToList()
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