using Microsoft.EntityFrameworkCore;
using Models;

public class Context:DbContext
{
    public DbSet<Maraton>Maratoni{get;set;}
    public DbSet<Trka>Trke{get;set;}
    public DbSet<Trkac>Trkaci{get;set;}
    
    
    public Context(DbContextOptions options)
    :base(options)
    {

    }
}