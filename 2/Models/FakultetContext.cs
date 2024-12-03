using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class FakultetContext:DbContext
    {
        public DbSet<Student>steudenti{get;set;}
        public DbSet<Predmet>predmeti{get;set;}
        public DbSet<IspitniRok>rokovi{get;set;}
        public DbSet<StudentPredmet>stdprd{get;set;}
        
        
        
        
        
        public FakultetContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

}