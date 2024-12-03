using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BioskopContext:DbContext
    {
        public DbSet<Sala>Sale{get;set;}
        public DbSet<Karta>Karte{get;set;}
        public DbSet<Projekcija>Projekcije{get;set;}
        public BioskopContext(DbContextOptions options)
        :base(options){}
          
        
        
        
    }
}