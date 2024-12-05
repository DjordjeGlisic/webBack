using Microsoft.EntityFrameworkCore;
namespace Models{
    public class Context:DbContext
    {
        public DbSet<Korisnik> Korisnici{get;set;}
        public DbSet<Tura> Ture{get;set;}
        public DbSet<Znamenitost> Znamenitosti{get;set;}
        public Context(DbContextOptions options)
        :base(options)
        {

        }
        

    }
}