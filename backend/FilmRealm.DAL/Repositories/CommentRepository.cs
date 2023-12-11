using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(FilmRealmContext context) : base(context)
    {
    }

    public async Task<List<Comment>> GetFilmComments(int filmId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.FilmId == filmId)
            .ToListAsync();
    }
}