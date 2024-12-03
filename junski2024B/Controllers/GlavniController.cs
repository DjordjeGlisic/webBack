using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers{
    [ApiController]
    [Route("[controller]")]
    public class GlavniController:ControllerBase
    {
        public Proslava _context{get;set;}
        public GlavniController(Proslava context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("DodajSalu")]
        public async Task<ActionResult> DodajSalu([FromBody] Sala s)
        {
            try
            {
                await _context.Sale.AddAsync(s);
                await _context.SaveChangesAsync();
                return Ok(s.Id);

            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DodajKorisnika")]
        public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik k)
        {
            try
            {
                await _context.Korisnici.AddAsync(k);
                await _context.SaveChangesAsync();
                return Ok(k.Id);

            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class Filter{
            public int Cena{get;set;}
            public int Kapacitet{get;set;}
            public string Zauzeta{get;set;}
             public string Datum{get;set;}
            
            
        }
          [HttpGet]
        [Route("VratiProlsave")]
        public async Task<ActionResult> VratiProlsave()
        {
            try
            {
                var sale = _context.Prolsave.Include(p => p.Sala).Include(p=>p.Korisnik);
             return Ok(sale.Select(p=>new{
                id=p.Id,
                sala=p.Sala,
                Korisnik=p.Korisnik,
                Datum=p.Dan.ToString()+"."+p.Mesec.ToString()+"."+p.Godina.ToString()
                
               
                
             }).ToList());
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("VratiSale")]
        public async Task<ActionResult> VratiSale()
        {
            try
            {
                var sale = _context.Sale;
                return Ok(sale.Select(p => new {
                    id = p.Id,
                    
                    kapacitet = p.Kapacitet,
                    cena = p.Cena,


                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("FilterSale")]
        public async Task<ActionResult> FilterSala([FromBody] Filter f)
        {
            try
            {
                var sale=_context.Sale.AsQueryable();
            
               

                   
                
                if(f.Cena!=-1)
                    sale=sale.Where(p=>p.Cena<=f.Cena);
                if(f.Kapacitet!=-1)
                    sale=sale.Where(p=>p.Kapacitet==f.Kapacitet);

                var lista = sale.ToList();

                var GodinaMesecDan = f.Datum.Split("-")
                .Where(p => int.TryParse(p, out _))
                .Select(int.Parse)
                .ToList();
                var saleZauzete = _context.Prolsave.Include(p => p.Sala).AsQueryable();
                saleZauzete = saleZauzete.Where(p => p.Godina == GodinaMesecDan[0]);
                saleZauzete = saleZauzete.Where(p => p.Mesec == GodinaMesecDan[1]);
                saleZauzete = saleZauzete.Where(p => p.Dan == GodinaMesecDan[2]);
                var listaZauzetih = saleZauzete.Select(p => p.Sala).ToList();
              
                if (listaZauzetih.Count > 0 && f.Zauzeta == "da")
                {
                    lista=lista.Where(p => listaZauzetih.Contains(p)==true).ToList();

                }
                else
                {
                   lista = lista.Where(p => listaZauzetih.Contains(p) == false).ToList();
                }
                return Ok(lista.Select(p=>new{
                    Id=p.Id,
                    Kapacitet=p.Kapacitet,
                    Adresa=p.Adresa,
                    Cena=p.Cena,
                    Iznajmljen=f.Zauzeta
                }).ToList());
                
                
                
                

            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("IznajmiSalu/{ImePrezime}/{Jmbg}/{IdSale}/{Datum}")]
        public async Task<ActionResult> Rezervacija([FromRoute] string ImePrezime,[FromRoute]long Jmbg,[FromRoute]int IdSale,[FromRoute]string Datum)
        {
            try
            {
                var sala=_context.Sale.Where(p=>p.Id==IdSale).FirstOrDefault();
                var ImePrez=ImePrezime.Split("-");
                var korisnik=_context.Korisnici.Where(p=>p.Ime==ImePrez[0]&&p.Prezime==ImePrez[1]&&p.Jmbg==Jmbg).FirstOrDefault();
                if(korisnik==null)
                    throw new Exception("Nije pronadjen");
                var GodinaMesecDan=Datum.Split("-")
                .Where(p=>int.TryParse(p,out _))
                .Select(int.Parse).ToList();
               
                var inzajmi=new Iznajmljena{

                    Sala=sala,
                    Korisnik=korisnik,
                    Godina=GodinaMesecDan[0],
                    Mesec=GodinaMesecDan[1],
                    Dan=GodinaMesecDan[2]

                };
                await _context.Prolsave.AddAsync(inzajmi);
                await _context.SaveChangesAsync();
                return Ok(inzajmi);

                

                   
             

               
               
                
                
                

            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}