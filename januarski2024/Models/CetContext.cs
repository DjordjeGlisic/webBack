using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class CetContext:DbContext
    {
        public DbSet<Korisnik> Korisnici{get;set;}
        public DbSet<Soba> Sobe{get;set;}
        public DbSet<Cet>Cetovi{get;set;}
        public CetContext(DbContextOptions options)
        :base(options)
        {}
        
        
    }
}