namespace Shared.QueryParameters;

public abstract class GetCollectionParameters
{
    const int maxLimit = 50;

    public int Page { get; set; } = 1;

    private int _limit = 10;
    public int Limit
    {
        get
        {
            return _limit;
        }
        set
        {
            _limit = value > maxLimit ? maxLimit : value;
        }
    }
}
