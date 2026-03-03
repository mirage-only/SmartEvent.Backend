namespace SmartEvent.Backend.Application.Common.Models;

public record PaginationParams
{
    private const int MaxPageSize = 100;
    private int _pageSize = 10;
    
    public int PageNumber { get; init; } = 1;

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value switch
        {
            > MaxPageSize => MaxPageSize,
            <= 0 => 10,
            _ => value
        };
    }
}