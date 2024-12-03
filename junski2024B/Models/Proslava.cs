using Microsoft.EntityFrameworkCore;
using Models;

public class Proslava:DbContext
{
    public DbSet<Korisnik>Korisnici{get;set;}
    public DbSet<Sala>Sale{get;set;}
    public DbSet<Iznajmljena>Prolsave{get;set;}
    public Proslava(DbContextOptions options)
    :base(options){}
    
        

}