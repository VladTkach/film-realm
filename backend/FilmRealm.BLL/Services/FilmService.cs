using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs;
using FilmRealm.Common.DTOs.Film;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using Sieve.Models;
using Sieve.Services;

namespace FilmRealm.BLL.Services;

public class FilmService : BaseService, IFilmService
{
    private readonly IImageService _imageService;
    private readonly IGenreRepository _genreRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly ISieveProcessor _sieveProcessor;

    public FilmService(IMapper mapper, IImageService imageService, IGenreRepository genreRepository,
        IActorRepository actorRepository, IFilmRepository filmRepository, ISieveProcessor sieveProcessor) : base(mapper)
    {
        _imageService = imageService;
        _genreRepository = genreRepository;
        _actorRepository = actorRepository;
        _filmRepository = filmRepository;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<FilmDto> GetFilmsByIdAsync(int filmId)
    {
        return _mapper.Map<FilmDto>(await _filmRepository.GetFilmInternalByIdAsync(filmId));
    }

    public PagedList<FilmDto> GetPagedFilms(SieveModel sieveModel)
    {
        var films = _filmRepository.GetFilms();
        var result = _sieveProcessor.Apply(sieveModel,  films, applyPagination: false);
        var total = result.Count();
        result = _sieveProcessor.Apply(sieveModel,  result, applyFiltering: false, applySorting: false);
        
        return new PagedList<FilmDto>
        {
            Items = _mapper.Map<List<FilmDto>>(result),
            TotalCount = total
        };
    }

    public async Task<FilmDto> AddFilmAsync(CreateFilmDto createFilmDto)
    {
        var film = _mapper.Map<Film>(createFilmDto);

        film.PosterId = await _imageService.AddPosterAsync(createFilmDto.Poster, null);

        foreach (var genreId in createFilmDto.GenresId)
        {
            film.Genres.Add(await _genreRepository.GetByIdAsync(genreId));
        }

        foreach (var actorId in createFilmDto.ActorsId)
        {
            film.Actors.Add(await _actorRepository.GetByIdAsync(actorId));
        }

        await _filmRepository.AddAsync(film);
        return _mapper.Map<FilmDto>(film);
    }

    public async Task DeleteFilmAsync(int filmId)
    {
        var film = await _filmRepository.GetByIdAsync(filmId);
        await _imageService.DeletePosterAsync(film.PosterId);

        _filmRepository.Delete(film);
    }
}