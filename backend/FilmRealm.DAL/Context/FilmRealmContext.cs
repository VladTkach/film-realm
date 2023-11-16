using FilmRealm.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Context;

public class FilmRealmContext : DbContext
{
    public FilmRealmContext(DbContextOptions<FilmRealmContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasMany(e => e.Films)
            .WithMany(e => e.Genres)
            .UsingEntity(j => j.ToTable("GenresRelations"));
        
        modelBuilder.Entity<Actor>()
            .HasMany(e => e.Films)
            .WithMany(e => e.Actors)
            .UsingEntity(j => j.ToTable("ActorsRelations"));
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Films)
            .WithMany(e => e.Users)
            .UsingEntity(j => j.ToTable("UserLists"));
        
        base.OnModelCreating(modelBuilder);
    }
}