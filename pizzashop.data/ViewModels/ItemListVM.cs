using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace pizzashop.data.ViewModels;

public class ItemListVM
{
    [Required]
    public int CategoryId { get; set; }

    public required PaginatedListVM<ItemtableVM> CatItem { get; set; } 
}

public class ItemCategory
{
    public int Id { get; set; }

    public string Name { get; set; }  = null!;

}