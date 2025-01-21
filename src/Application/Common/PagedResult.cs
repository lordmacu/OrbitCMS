public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int Total { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
}
