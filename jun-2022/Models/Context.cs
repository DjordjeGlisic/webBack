using Microsoft.EntityFrameworkCore;

namespace Models{
    public class Context:DbContext
    {
        public DbSet<Auto>Auta{get;set;}
        public DbSet<Brend>Brendovi{get;set;}
        public DbSet<Prodavnica>Prodavnice{get;set;}
        public Context(DbContextOptions options)
        :base(options){}
        
        
    }
}