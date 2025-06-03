namespace pizzashop.data.ViewModels;

public class PaginatedListVM<T>
{

    public List<T> Items { get; }
    public int PageIndex { get; }

    public int PageSize { get; }
    public int TotalPages { get; }

    public string Search { get;}

    public int TotalItems { get; }
   
    public PaginatedListVM(List<T> items, int pageIndex, int totalPages , int pagesize , string search="", int totalItems = 0)
    {
        Items = items;
        PageSize =pagesize;
        PageIndex = pageIndex;
        TotalPages = totalPages;
        Search = search;
        TotalItems = totalItems;
    }
}

