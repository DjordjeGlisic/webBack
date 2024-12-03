using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
[ApiController]
[Route("[controller]")]
public class GlavniController:ControllerBase
{

    public Context _context{get; set;} 
    public GlavniController(Context context)
    {
        _context = context;
    }
    [HttpPost]
    [Route("DodajTakmicara")]
    public async Task<IActionResult> PostTakmicar([FromBody]Trkac t)
    {
        try
        {
            await _context.Trkaci.AddAsync(t);
            await _context.SaveChangesAsync();
            return Ok(t.Id);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet]
    [Route("VratiTakmicare/{ID}")]
    public async Task<IActionResult> GetTakmicari([FromRoute]int ID)
    {
        try
        {
            var  takmicari=_context.Trke.Include(p=>p.Takmicenje).Include(p=>p.Maratonac).Where(p=>p.Takmicenje.Id==ID);
            return Ok(
                takmicari.Select(p=>new{
                    Id=p.Maratonac.Id,
                    Ime=p.Maratonac.Ime,
                    Prezime=p.Maratonac.Prezime

                }).ToList()
        );

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet]
    [Route("VratiTakmicara/{ID}")]
    public async Task<IActionResult> GetTakmicar([FromRoute]int ID)
    {
        try
        {
            var  takmicar=_context.Trkaci.Include(p=>p.Maratoni).Where(p=>p.Id==ID).FirstOrDefault();
            float sum=0.0f;
            foreach(var takmicenje in takmicar.Maratoni)
            {
                sum+=takmicenje.BrzinaTrkaca;
            }
            float srednja=0.0f;;
            if(takmicar.Maratoni.Count!=0)
                srednja=sum/takmicar.Maratoni.Count;
            return Ok(
                new{
                 Id=takmicar.Id,
                 ImePrezime=takmicar.Ime+" "+takmicar.Prezime,
                 BrojNagrada=takmicar.Nagrade,
                 SrednjaBrzina=srednja

                }
        );

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet]
    [Route("VratiMaraton/{ID}")]
    public async Task<IActionResult> Maraton([FromRoute]int ID)
    {
        try
        {
            var  maraton=_context.Maratoni.Where(p=>p.Id==ID).FirstOrDefault();
            return Ok(
                new{
                 Id=maraton.Id,
                 Naziv=maraton.Naziv,
                 Lokacija=maraton.Lokacija
                }
        );

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

     [HttpPost]
    [Route("DodajMaraton")]
    public async Task<IActionResult> PostMaraton([FromBody]Maraton  m)
    {
        try
        {
            await _context.Maratoni.AddAsync(m);
            await _context.SaveChangesAsync();
            return Ok(m.Id);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpPost]
    [Route("DodajTakmicaraMaratonu/{Takmicar}/{Maraton}/{Broj}/{Pozicija}")]
    public async Task<IActionResult> DodajTrku([FromRoute]int Takmicar,[FromRoute]int Maraton,[FromRoute]int Broj,[FromRoute]int Pozicija)
    {
        try
        {
            var t=_context.Trkaci.Where(p=>p.Id==Takmicar).FirstOrDefault();
            var m=_context.Maratoni.Where(p=>p.Id==Maraton).FirstOrDefault();
       
            var trka=new Trka{
                BrojMaratonca=Broj,
                Pozicija=Pozicija,
                PredjenoMetra=0,
                Progres=0,
                BrzinaTrkaca=0,
                Maratonac=t,
                Takmicenje=m
                };
             await   _context.Trke.AddAsync(trka);
            await _context.SaveChangesAsync();
            return Ok(trka.Id);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    public class Podaci{
       public int Mesto{get;set;}
        public int DodajMetre{get;set;}
        public float Brzina{get;set;}
    }
    [HttpPut]
    [Route("UnaprediKorisnika/{IDMaratona}/{IDTrkaca}")]
    public async Task<IActionResult> UnaprediKorisnika([FromRoute]int IDMaratona,[FromRoute]int IDTrkaca,[FromBody]Podaci podaci)
    {
        try
        {
            var trka=_context.Trke.Include(p=>p.Maratonac).Include(p=>p.Takmicenje).Where(p=>p.Takmicenje.Id==IDMaratona&&p.Maratonac.Id==IDTrkaca).FirstOrDefault();
            if(trka==null){return BadRequest("Nije pronadjena trka");}
            trka.Pozicija=podaci.Mesto;
            trka.PredjenoMetra+=podaci.DodajMetre;  
            trka.BrzinaTrkaca+=podaci.Brzina;
            float decimalno=trka.PredjenoMetra/trka.Takmicenje.DuzinaStazeM;
            decimalno*=100;
            trka.Progres=(int)decimalno;
            await _context.SaveChangesAsync();
            return Ok("Uspesno azuriran korisnik");


        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet]
    [Route("VratiInformacije/{ID}/{Vreme}")]
    public async Task<IActionResult> GetInfo([FromRoute]int ID,[FromRoute]string Vreme)
    {
        try
        {
            var  trka=_context.Trke.Include(p=>p.Takmicenje).Include(p=>p.Maratonac).Where(p=>p.Maratonac.Id==ID).FirstOrDefault();
            var vreme=Vreme.Split(":")
            .Where(p=>int.TryParse(p,out _))
            .Select(int.Parse)
            .ToList();
            var sati=vreme[0];
            var minuti=vreme[1];
            var pocetakTrke=trka.Takmicenje.VremePocetkaTrke.Split(":")
            .Where(p=>int.TryParse(p,out _))
            .Select(int.Parse)
            .ToList();
            var krajTrke=trka.Takmicenje.VremeKrajaTrke.Split(":")
            .Where(p=>int.TryParse(p,out _))
            .Select(int.Parse)
            .ToList();
            if(sati<pocetakTrke[0]||sati>krajTrke[0])
            {
                return BadRequest("Uneli ste vreme koje je van opsega vremenskog trke");
            }
            string delta=$"{sati-pocetakTrke[0]}:{(minuti-pocetakTrke[1])}";
            
            
            return Ok(new{
                Pocetak=trka.Takmicenje.VremePocetkaTrke,
                Duzina=trka.Takmicenje.DuzinaStazeM,
                Broj=trka.Pozicija,
                Predjeno=trka.PredjenoMetra,
                Vreme=delta,
                Prosecna=trka.BrzinaTrkaca,
                Progres=trka.Progres
            });
            
       

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}