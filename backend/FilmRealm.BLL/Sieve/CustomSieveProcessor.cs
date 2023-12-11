using FilmRealm.DAL.Entities;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace FilmRealm.BLL.Sieve;

public class CustomSieveProcessor : SieveProcessor
{
    public CustomSieveProcessor(IOptions<SieveOptions> options, ISieveCustomFilterMethods customFilterMethods) :
        base(options, customFilterMethods)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<Film>(f => f.Year)
            .CanSort();

        mapper.Property<Film>(f => f.Name)
            .CanSort()
            .CanFilter();

        return mapper;
    }
}