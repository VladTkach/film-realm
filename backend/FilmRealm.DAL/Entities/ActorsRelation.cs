using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class ActorsRelation : BaseEntity
{
    public int FilmId { get; set; }
    public int ActorId { get; set; }
    public Film Film { get; set; } = null!;
    public Actor Actor { get; set; } = null!;
}