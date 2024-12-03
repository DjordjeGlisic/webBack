using Microsoft.EntityFrameworkCore;

namespace Models{
    public class PortalContext:DbContext
    {
        public DbSet<Recept>Recepti{get;set;}
        public DbSet<Sastojak>Sastojci{get;set;}
        public DbSet<SastojakRecept>SastojakRecepts{get;set;}
        public PortalContext(DbContextOptions options)
        :base(options)
        {
            
        }
        
        
    }
}