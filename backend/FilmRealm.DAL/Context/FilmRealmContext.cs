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
    public DbSet<UserList> UserLists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<GenresRelation> GenresRelations { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<ActorsRelation> ActorsRelations { get; set; }
}