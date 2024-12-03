using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ZadatakContext:DbContext
    {
       public DbSet<Stan>Stanovi{get;set;}
        public DbSet<Racun>Racuni{get;set;}    
        public ZadatakContext(DbContextOptions options)
        :base(options)
        {}

    }
    
}