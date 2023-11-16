using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class Film : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Year { get; set; }
    public Guid PosterId { get; set; }
    public Guid ResourceId { get; set; }
    
    public List<GenresRelation> GenresRelations { get; } = new();
    public List<Genre> Genres { get; } = new();
    
    public List<ActorsRelation> ActorsRelations { get; } = new();
    public List<Actor> Actors { get; } = new();
    
    public List<UserList> UserLists { get; set; } = new();
    public List<User> Users { get; set; } = new();
}