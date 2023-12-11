using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs.Genre;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.Shared.Exceptions;

namespace FilmRealm.BLL.Services;

public class GenreService : BaseService, IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository, IMapper mapper) : base(mapper)
    {
        _genreRepository = genreRepository;
    }
    public async Task<List<GenreDto>> GetAllGenresAsync()
    {
        return _mapper.Map<List<GenreDto>>(await _genreRepository.GetAllAsync());
    }

    public async Task<GenreDto> AddGenreAsync(CreateGenreDto createGenreDto)
    {
        if (await _genreRepository.GetGenreByNameAsync(createGenreDto.Name) is not null)
        {
            throw new EntityAlreadyExistException(nameof(Genre));
        }

        var genre = await _genreRepository.AddAsync(_mapper.Map<Genre>(createGenreDto));
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task<GenreDto> UpdateGenreAsync(UpdateGenreDto updateGenreDto)
    {
        var genre = await _genreRepository.GetByIdAsync(updateGenreDto.Id);
        
        _mapper.Map(updateGenreDto, genre);
        _genreRepository.Update(genre);
        
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task DeleteGenreAsync(int genreId)
    {
        await _genreRepository.GetByIdAsync(genreId);
        await _genreRepository.DeleteByIdAsync(genreId);
    }
}