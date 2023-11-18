using AutoMapper;

namespace FilmRealm.BLL.Services.Abstract;

public abstract class BaseService(IMapper mapper)
{
    protected readonly IMapper _mapper = mapper;
}