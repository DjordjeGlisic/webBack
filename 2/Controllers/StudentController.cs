using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Controllers
{


[ApiController]
[Route("[controller]")]
public class StudentController: ControllerBase
{
    public FakultetContext Context { get; set; }    
    public StudentController(FakultetContext context)
    {
        Context=context;


    }
    [HttpGet]
    [Route("VratiStudente")]
    public async  Task<ActionResult> VratiStudente([FromQuery] string[] rokovi)
    {
        try
        {
           var studenti=Context.steudenti
           .Include(p=>p.StudentPredmet)
           .ThenInclude(p=>p.Rok)
           .Include(p=>p.StudentPredmet)
           .ThenInclude(p=>p.Predmet);
           var student=studenti;/*.Where(p=>p.ID==4).FirstOrDefault();*/
           /*var student=Context.steudenti.Where(p=>p.ID==4).FirstOrDefault();
           await Context.Entry(student).Collection(p=>p.StudentPredmet).LoadAsync();
           foreach (var s in student.StudentPredmet)
           {
                await Context.Entry(s).Reference(q=>q.Predmet).LoadAsync();
                await Context.Entry(s).Reference(q=>q.Rok).LoadAsync();  
           }*/
            //return Ok(student);                   
                                        
            return Ok(
                student.Select(p=>
                new {
                    Indeks = p.Indeks,
                    Ime = p.Ime,
                    Prezime=p.Prezme,
                    Predmeti= p.StudentPredmet.Where((z=>rokovi.Contains(z.Rok.NazivRoka))).Select(q=>
                    new
                    {
                        Naziv=q.Predmet.Naziv,
                        Ocena=q.Ocena,
                        Rok=q.Rok.NazivRoka
                    })

                }).ToList()
            );


        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);  
        }
    }
    [HttpGet]
    [Route("VratiStudenteSaPredmetom/{Predmet}/{Rokovi}")]
    public async  Task<ActionResult> VratiStudenteSaPredmetom([FromRoute] int Predmet,[FromRoute] string Rokovi)
    {
        try
        {
            var idRokova=Rokovi.Split("a")
            .Where(p=>int.TryParse(p,out _))
            .Select(int.Parse)
            .ToList();
           var studenti=Context.stdprd
           .Include(p=>p.Student)
           .Include(p=>p.Predmet)
           .Include(p=>p.Rok)
           .Where(p=>idRokova.Contains(p.Rok.Id)&&p.Predmet.Id==Predmet);
           var filter=studenti;
            return Ok(
                filter.Select(p=>
                new {
                    Id=p.Id,
                    Indeks = p.Student.Indeks,
                    Ime = p.Student.Ime,
                    Prezime=p.Student.Prezme,
                    Predmet=p.Predmet.Naziv,
                    Rok=p.Rok.NazivRoka,
                    Ocena=p.Ocena

                }).ToList()
            );


        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);  
        }
    }
     [HttpPost]
    [Route("StudentiFromBody/{Predmet}")]
    public async  Task<ActionResult> Body([FromRoute] int Predmet,[FromBody]int[]rokoviID)
    {
        try
        {
            
           var studenti=Context.stdprd
           .Include(p=>p.Student)
           .Include(p=>p.Predmet)
           .Include(p=>p.Rok)
           .Where(p=>rokoviID.Contains(p.Rok.Id)&&p.Predmet.Id==Predmet);
           var filter=studenti;
            return Ok(
                filter.Select(p=>
                new {
                    Indeks = p.Student.Indeks,
                    Ime = p.Student.Ime,
                    Prezime=p.Student.Prezme,
                    Predmet=p.Predmet.Naziv,
                    Rok=p.Rok.NazivRoka,
                    Ocena=p.Ocena

                }).ToList()
            );


        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);  
        }
    }
    
    [HttpGet]
    [Route("VratiStudenta/{ID}")]
    public async Task<ActionResult<Student>> Get([FromRoute]int ID)
    {
        var student=await Context.steudenti.FindAsync(ID);
        if (student == null)
            return BadRequest("Student nije pronadjen");
        return Ok(student);
    }  
    [HttpPost]
    [Route("DodajStudenta")]
    public async Task<ActionResult> DodajStudenta(Student s)
    {
        if(s.Indeks<10000||s.Indeks>20000)
            return BadRequest("Pogresan broj indeksa");
        Context.steudenti.Add(s);
        await Context.SaveChangesAsync();
        return Ok(s.ID);
    }
    [HttpPut]
    [Route("AzurirajStudenta/{Indeks}")]
    public async Task<ActionResult> AzurirajStudenta([FromRoute]int Indeks,[FromBody]Student s)
    {
        if(Indeks<10000||Indeks>20000)
            return BadRequest("ID mora biti izmedju 10000 i 20000");
        var std=Context.steudenti.Where(p=>p.Indeks==Indeks).FirstOrDefault();
         
        //Context.steudenti.Remove(std);
        if(s.Ime!=null)
            std.Ime=s.Ime;
        if(s.Prezme!=null)
            std.Prezme=s.Prezme;    
        if(s.Indeks>10000&&s.Indeks<20000)
            std.Indeks=s.Indeks;
        //await Context.steudenti.AddAsync(std);
        await Context.SaveChangesAsync();   
        return Ok("Uspesno azuriran student");

    }
    [HttpDelete]
    [Route("ObrisiStudenta/{Id}")]
    public async Task<ActionResult> ObrisiStudenta([FromRoute]int Id)
    {
        try{
        /*if(Indeks<10000||Indeks>20000)
            return BadRequest("Ne validan Indeks");*/
        var zaBrisanje=await Context.stdprd.Include(p=>p.Student).Include(p=>p.Predmet).Include(p=>p.Rok).Where(p=>p.Id==Id).FirstOrDefaultAsync();
        if(zaBrisanje==null)
            return BadRequest("dati student ne posoji");
        long index=zaBrisanje.Student.Indeks;  
        Context.stdprd.Remove(zaBrisanje);
        await Context.SaveChangesAsync();
        return Ok($"Uspesno brisanje reda iz tabele");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    [Route("PromeniStudenta/{Indeks}/{Ime}/{Prezime}")]
    public async Task<ActionResult> PromeniStudenta([FromRoute] long Indeks,[FromRoute]string Ime,[FromRoute] string Prezime)
    {
        if(Indeks<10000||Indeks>20000)
            return BadRequest("Nevalidan indeks");
            try
            {
            var student=Context.steudenti.Where(s => s.Indeks==Indeks).FirstOrDefault();
            if(student==null)
                return BadRequest("Student sa prosledjenim indeksom nije pronadjen");   
            if(Ime.Length>50 ||string.IsNullOrWhiteSpace(Ime))
                return BadRequest("Ne postojece ime");
             if(Prezime.Length>50 ||string.IsNullOrWhiteSpace(Prezime))
                return BadRequest("Ne postojece prezime");
            student.Ime=Ime;
            student.Prezme=Prezime; 
            await Context.SaveChangesAsync();   
            return Ok(student);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }
    [HttpPut]
    [Route("PromenaFromBody")]
    public async Task<ActionResult> PromeniFromBody([FromBody]Student s)
    {
        try
        {
            if(s.Indeks<10000||s.Indeks>20000||s.ID<0||s.ID>10000)
                return BadRequest("Nevalidan indeks ili ID ili oba");
            var student=Context.steudenti.Where(p=>p.ID==s.ID&&p.Indeks==s.Indeks).FirstOrDefault();
            if(student==null)
                return BadRequest("Student sa navedenim imenom i prezimenom nije pronadjen");
           string ime=student.Ime;
            student.Ime=s.Ime;
            student.Prezme=s.Prezme;
            await Context.SaveChangesAsync();
             return Ok($"Uspesno promenjen student sa imenom {ime}");

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
}