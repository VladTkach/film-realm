using FilmRealm.Common.DTOs.Actor;

namespace FilmRealm.BLL.Interfaces;

public interface IActorService
{
    Task<List<ActorDto>> GetAllActorsAsync();
    Task<ActorDto> AddActorAsync(CreateActorDto createActorDto);
    Task<ActorDto> UpdateActorAsync(UpdateActorDto updateActorDto);
    Task DeleteActorAsync(int actorId);
}