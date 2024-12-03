using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IznajmContext:DbContext
    {
        public DbSet<Korisnik> Korisnici{get;set;}
        public DbSet<Automobil> Automobili{get;set;}
        public DbSet<Model> Modeli{get;set;}
        public IznajmContext(DbContextOptions options)
        :base(options)
        {

        }
        
        
    }

}