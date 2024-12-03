using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers{
    [ApiController]
    [Route("[controller]")]
    public class ProizvodController:ControllerBase
    {
        public ZadatakContext _context{get;set;}
        public ProizvodController(ZadatakContext context)
        {
            _context=context;
        }
        public class ProizvodDTO
        {
         
        public string Naziv{get;set;}
        public Kategorija KategorijaProizvoda{get;set;}
        public float Cena{get;set;}
        public int Kolicina{get;set;}
    
        }
        [HttpPost]
        [Route("DodajProizvodProdavnici/{IdProdavnice}")]
        public async Task<ActionResult>DodajProdavnicu([FromRoute]int IdProdavnice,[FromBody]ProizvodDTO p)
        {
            try
            {
                int id=-1;
                if(string.IsNullOrEmpty(p.Naziv)||p.Naziv.Length>50)
                    throw new Exception("Niste uneli validan naziv");
                if(p.Cena<0||p.Cena>1000)
                    throw new Exception("Niste uneli validnu cenu");
                if(p.Kolicina<0||p.Kolicina>100)
                    throw new Exception("Niste uneli validnu kolicinu");
                var prodavnica=_context.Prodavnice.Include(p=>p.ListaProizvoda).Where(p=>p.Id==IdProdavnice).FirstOrDefault();
                if(prodavnica==null)
                    throw new Exception("Prodavnica nije nadjena");
                var postoji=prodavnica.ListaProizvoda.Where(q=>q.Naziv==p.Naziv).FirstOrDefault();
                if(postoji!=null)
                {
                    if(postoji.Kolicina+p.Kolicina>100)
                        throw new Exception("Premasena je maksimalna kolicina proizvoda u datoj prodavnici");
                    postoji.Kolicina+=p.Kolicina;
                    id=postoji.Id;
                    await _context.SaveChangesAsync();



                }
                else
                {
                    var proizvod=new Proizvod{
                        Naziv=p.Naziv,
                        KategorijaProizvoda=p.KategorijaProizvoda,
                        Cena=p.Cena,
                        Kolicina=p.Kolicina,
                        PripadaProdavnici=prodavnica
                    };
                    await _context.Proizvodi.AddAsync(proizvod);
                    await _context.SaveChangesAsync();

                    id=proizvod.Id;
                    
                }

                return Ok(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("ProdajProizvodeizProdavnice/{IdProizvoda}/{Kolicina}")]
        public async Task<ActionResult>Prodaj([FromRoute]int IdProizvoda,[FromRoute]int Kolicina)
        {
            try
            {
                
                if(IdProizvoda<0||IdProizvoda>1000)
                    throw new Exception("Niste uneli validan id proizvoda");
           
               var proizvod=_context.Proizvodi.Where(p=>p.Id==IdProizvoda).FirstOrDefault();   
                 if(proizvod==null)
                    throw new Exception("Proizvod nije pronadjen");
                proizvod.Kolicina-=Kolicina;
                await _context.SaveChangesAsync();  
                return Ok(proizvod.Kolicina);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}