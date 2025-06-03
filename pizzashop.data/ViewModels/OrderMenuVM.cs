namespace pizzashop.data.ViewModels;

// category VM for sidebar
public class MenuCategories{
    public int OrderId { get; set; }

    public List<CategoryVM> Cateogries { get; set; } = new List<CategoryVM>();
}


public class CategoryItems{
    public int Id { get; set;}
    public string Name { get; set;} = null!;
    public string Img { get; set;} = null!;
    public string Type { get; set;} = null!;
    public bool IsFav { get; set;}
    public float Price { get; set;}
}


public class ItemGroups{
   
    public int Min { get; set; }
    public int Max { get; set; }
    public string GroupName { get; set; } = null!;

    public List<GroupMods> Modifiers { get; set; } = new List<GroupMods>();
}

public class GroupMods{
     public int ModifierId { get; set; }

    public string Name { get; set; } = null!;

    public float Price { get; set; }


}


public class OrderDisplay {
    public int Orderid { get; set; }
    public int CustomerId { get; set; }
    public string Instruction { get; set; } = null!;
    
    public string OrderStauts { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;
    public string FloorName { get; set; }= null!;
    public List<string> TableNames { get; set; } = new List<string>();
    public List<OrderDetailsVM> Items { get; set; } = new List<OrderDetailsVM>();

    public List<OrderTaxis> Taxes { get; set; } = new List<OrderTaxis>();
}



public class OrderDetailsVM{
    public int OrderId { get; set; }
    public int DetailsId { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }

    public int Prepared { get; set; }
    public float Price { get; set; }
    public string Instructions { get; set; } = null!;
    public bool? DefaultTax { get; set; }

    public short? TaxPercentage { get; set; }

    public List<OrderModifiers> Modifiers { get; set; } = new List<OrderModifiers>();
}

public class OrderModifiers{
    public int ModiferId { get; set; }
    public string Name { get; set; } = null!;
    public float Price { get; set; }
}

public class OrderTaxis{
    public int OrderTaxid { get; set; }
    public int TaxId { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsDefault { get; set; }
    public float Amount { get; set; }
    
}


public class OrderCustomer{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Ppl { get; set; }
}


public class OrderReview{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public short Food { get; set; }
    public short Service { get; set; }
    public short Ambience { get; set; }
    public string Descript { get; set; } = null!;
}


public class UpdateOrderDetails{
    public int DetailsId { get; set; }
    public int OrderId { get; set; }
    public int Itemid { get; set; }
    public int Quantity { get; set; }
    public string Instruction { get; set; }= null!;
    
    public List<int> ModifierIds { get; set; } = new List<int>();
}