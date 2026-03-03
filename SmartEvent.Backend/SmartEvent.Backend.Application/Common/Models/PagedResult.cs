namespace SmartEvent.Backend.Application.Common.Models;

public class PagedResult<T>(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalSize { get; set; } = totalItems;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages =>  (int)Math.Ceiling((double)TotalSize / PageSize); 
    public bool  HasPreviousPage => PageNumber > 1;
    public bool  HasNextPage => PageNumber < TotalPages;
}