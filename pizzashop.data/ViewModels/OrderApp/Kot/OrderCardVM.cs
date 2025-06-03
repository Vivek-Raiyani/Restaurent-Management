namespace pizzashop.data.ViewModels.OrderApp.Kot;

public class PaginatedKotVM<T>
{

    public IEnumerable<T> Items { get; }
    public int PageIndex { get; }

    public int PageSize { get; }
    public int TotalPages { get; }

    public string Status { get;  }
   
    public PaginatedKotVM(IEnumerable<T> items, int pageIndex, int totalPages , int pagesize , string status )
    {
        Items = items;
        PageSize =pagesize;
        PageIndex = pageIndex;
        TotalPages = totalPages;
        Status = status;
    }
}


public class OrderModalVM
{
    public int Orderid { get; set; }

    public virtual ICollection<KotItemVM> Items { get; set; } = new List<KotItemVM>();

}

public class OrderCardVM
{
    public int Orderid { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Section { get; set; } = null!;

    public List<string> Tabels { get; set; } = new List<string>();

    public virtual ICollection<KotItemVM> Items { get; set; } = new List<KotItemVM>();

    public string OrderInstruction { get; set; } = null!;
}

public class KotItemVM {

    public int OrderDetailsid { get; set; }

    public int Categoryid { get; set; }

    public string Item { get; set; } = null!;

    public int Quantity { get; set; }

    public int Prepared { get; set; }
    public string Instructions { get; set;} = null!;

    public virtual ICollection<KotModifiers> Modifier { get; set; } = new List<KotModifiers>();

}

public class KotModifiers {

    public int Modiferid { get; set; }

    public string Name { get; set; } = null!;

    
}

public class KotOrderUpdate{

    public int Orderid { get; set;}

    public string Status { get; set; } = null!;

    public List<KotItemUpdate> Detailsids { get; set; } = new List<KotItemUpdate>();
}

public class KotItemUpdate{

    public int Detailsid { get; set; }

    public int Quantity { get; set; }
}