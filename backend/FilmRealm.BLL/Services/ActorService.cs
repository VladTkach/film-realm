using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs.Actor;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.Shared.Exceptions;

namespace FilmRealm.BLL.Services;

public class ActorService(IMapper mapper, IActorRepository actorRepository) : BaseService(mapper), IActorService
{
    
    public async Task<List<ActorDto>> GetAllActorsAsync()
    {
        return _mapper.Map<List<ActorDto>>(await actorRepository.GetAllAsync());
    }

    public async Task<ActorDto> AddActorAsync(CreateActorDto createActorDto)
    {
        if (await actorRepository.GetActorByNameAsync(createActorDto.Name) is not null)
        {
            throw new EntityAlreadyExistException(nameof(Actor));
        }

        var actor = await actorRepository.AddAsync(_mapper.Map<Actor>(createActorDto));
        return _mapper.Map<ActorDto>(actor);
    }

    public async Task<ActorDto> UpdateActorAsync(UpdateActorDto updateActorDto)
    {
        var actor = await actorRepository.GetByIdAsync(updateActorDto.Id);
        
        _mapper.Map(updateActorDto, actor);
        actorRepository.Update(actor);
        
        return _mapper.Map<ActorDto>(actor);
    }

    public async Task DeleteActorAsync(int actorId)
    {
        await actorRepository.GetByIdAsync(actorId);
        await actorRepository.DeleteByIdAsync(actorId);
    }
}