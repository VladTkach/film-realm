namespace FilmRealm.Common.DTOs;

public class PagedList<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
}