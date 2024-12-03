using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProdavnicaContext:DbContext
    {
        public DbSet<Kategorija>Kategorije{get;set;}
        public DbSet<Korpa> Korpe{get;set;}
        
        public DbSet<Proizvod>Proizvodi{get;set;}
        public ProdavnicaContext(DbContextOptions options)
        :base(options)
        {

        }
       
    }
}