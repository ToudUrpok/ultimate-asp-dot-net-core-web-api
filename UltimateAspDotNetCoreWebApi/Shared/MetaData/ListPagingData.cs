namespace Shared.MetaData;

public record ListPagingData
{
    public ListPagingData(int page, int limit, int totalCount)
    {
        Page = page;
        Limit = limit;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)limit);
    }

    public int Page { get; init; }
    public int Limit { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
}
