using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet("all/{filmId}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CommentDto>>> GetAllCommentsAsync(int filmId)
    {
        return Ok(await _commentService.GetFilmCommentsAsync(filmId));
    }
    
    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddCommentAsync(CreateCommentDto createCommentDto)
    {
        return Ok(await _commentService.AddNewCommentAsync(createCommentDto));
    }
    
    [HttpPut]
    public async Task<ActionResult<CommentDto>> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
    {
        return Ok(await _commentService.UpdateCommentAsync(updateCommentDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCommentAsync(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}