using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> GetFilmComments(int filmId);
}