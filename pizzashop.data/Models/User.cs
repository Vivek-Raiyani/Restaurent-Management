using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Fname { get; set; } = null!;

    public string? Lname { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfileImg { get; set; }

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public bool Status { get; set; }

    public int RoleId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Customer> CustomerCreatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Customer> CustomerUpdateByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<MenuCategory> MenuCategoryCreatedByNavigations { get; set; } = new List<MenuCategory>();

    public virtual ICollection<MenuCategory> MenuCategoryUpdatedByNavigations { get; set; } = new List<MenuCategory>();

    public virtual ICollection<MenuItem> MenuItemCreatedByNavigations { get; set; } = new List<MenuItem>();

    public virtual ICollection<MenuItem> MenuItemUpdatedByNavigations { get; set; } = new List<MenuItem>();

    public virtual ICollection<Modifier> ModifierCreatedByNavigations { get; set; } = new List<Modifier>();

    public virtual ICollection<ModifierGroup> ModifierGroupCreatedByNavigations { get; set; } = new List<ModifierGroup>();

    public virtual ICollection<ModifierGroup> ModifierGroupUpdatedByNavigations { get; set; } = new List<ModifierGroup>();

    public virtual ICollection<Modifier> ModifierUpdatedByNavigations { get; set; } = new List<Modifier>();

    public virtual ICollection<Order> OrderCreatedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUpdatedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Permission> PermissionCreatedByNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<Permission> PermissionUpdatedByNavigations { get; set; } = new List<Permission>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Section> SectionCreatedByNavigations { get; set; } = new List<Section>();

    public virtual ICollection<Section> SectionUpdatedByNavigations { get; set; } = new List<Section>();

    public virtual ICollection<TableDetail> TableDetailCreatedByNavigations { get; set; } = new List<TableDetail>();

    public virtual ICollection<TableDetail> TableDetailUpdatedByNavigations { get; set; } = new List<TableDetail>();

    public virtual ICollection<Taxis> TaxisCreatedByNavigations { get; set; } = new List<Taxis>();

    public virtual ICollection<Taxis> TaxisModifiedByNavigations { get; set; } = new List<Taxis>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<Waitlist> WaitlistCreatedByNavigations { get; set; } = new List<Waitlist>();

    public virtual ICollection<Waitlist> WaitlistUpdatedByNavigations { get; set; } = new List<Waitlist>();
}
