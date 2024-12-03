using Microsoft.EntityFrameworkCore;

namespace Models{
    public class ZadatakContext:DbContext
    {
        public DbSet<Prodavnica>Prodavnice{get;set;}
        public DbSet<Proizvod>Proizvodi{get;set;}
        public ZadatakContext(DbContextOptions options)
        :base(options)
        {

        }
        
        
    }
}