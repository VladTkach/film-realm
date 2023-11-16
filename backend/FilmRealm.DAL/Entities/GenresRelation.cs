using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class GenresRelation : BaseEntity
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }
    public Film Film { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}