using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pizzashop.data.Models;

public partial class PizzashopContext : DbContext
{
    public PizzashopContext()
    {
    }

    public PizzashopContext(DbContextOptions<PizzashopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventsetup> Eventsetups { get; set; }

    public virtual DbSet<ItemModifierGroup> ItemModifierGroups { get; set; }

    public virtual DbSet<MenuCategory> MenuCategories { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Modifier> Modifiers { get; set; }

    public virtual DbSet<ModifierGroup> ModifierGroups { get; set; }

    public virtual DbSet<ModifierGroupMapping> ModifierGroupMappings { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItemModifier> OrderItemModifiers { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }

    public virtual DbSet<OrderTax> OrderTaxes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    public virtual DbSet<ResetToken> ResetTokens { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<TableDetail> TableDetails { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitlist> Waitlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Pizzashop;Username=postgres;Password=12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("menu_item_type", new[] { "veg", "nonveg", "vegan" })
            .HasPostgresEnum("order_stat", new[] { "pending", "inprogress", "ready" })
            .HasPostgresEnum("order_typ", new[] { "dine in", "parcle" })
            .HasPostgresEnum("pay_method", new[] { "upi", "cash", "card" })
            .HasPostgresEnum("table_stat", new[] { "occupied", "running", "reserved" })
            .HasPostgresEnum("tax_typ", new[] { "percentage", "fixed" })
            .HasPostgresEnum("unit_size", new[] { "g", "pieces" });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .HasColumnName("phone_no");
            entity.Property(e => e.UpdateBy).HasColumnName("update_by");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("customers_created_by_fkey");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CustomerUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("customers_update_by_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Ac).HasColumnName("ac");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Dinein)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("dinein");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.EventDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("event_date");
            entity.Property(e => e.EventType)
                .HasMaxLength(40)
                .HasColumnName("event_type");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Persons).HasColumnName("persons");
            entity.Property(e => e.SetupId).HasColumnName("setup_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Events)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_customerid");

            entity.HasOne(d => d.Order).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_orderid");

            entity.HasOne(d => d.Setup).WithMany(p => p.Events)
                .HasForeignKey(d => d.SetupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventsetup_setup_id");
        });

        modelBuilder.Entity<Eventsetup>(entity =>
        {
            entity.HasKey(e => e.SetupId).HasName("eventsetup_pkey");

            entity.ToTable("eventsetup");

            entity.Property(e => e.SetupId).HasColumnName("setup_id");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.Items)
                .HasMaxLength(40)
                .HasColumnName("items");
            entity.Property(e => e.Setup)
                .HasMaxLength(40)
                .HasColumnName("setup");
        });

        modelBuilder.Entity<ItemModifierGroup>(entity =>
        {
            entity.HasKey(e => e.ItemModId).HasName("item_modifier_group_pkey");

            entity.ToTable("item_modifier_group");

            entity.Property(e => e.ItemModId).HasColumnName("item_mod_id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Maximum)
                .HasDefaultValueSql("1")
                .HasColumnName("maximum");
            entity.Property(e => e.Minimun)
                .HasDefaultValueSql("1")
                .HasColumnName("minimun");
            entity.Property(e => e.ModiferId).HasColumnName("modifer_id");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemModifierGroups)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_modifier_group_item_id_fkey");

            entity.HasOne(d => d.Modifer).WithMany(p => p.ItemModifierGroups)
                .HasForeignKey(d => d.ModiferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_modifier_group_modifer_id_fkey");
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("menu_categories_pkey");

            entity.ToTable("menu_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MenuDescription)
                .HasMaxLength(255)
                .HasColumnName("menu_description");
            entity.Property(e => e.MenuName)
                .HasMaxLength(20)
                .HasColumnName("menu_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuCategoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("menu_categories_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MenuCategoryUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("menu_categories_updated_by_fkey");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.IteamId).HasName("menu_items_pkey");

            entity.ToTable("menu_items");

            entity.Property(e => e.IteamId).HasColumnName("iteam_id");
            entity.Property(e => e.Available)
                .HasDefaultValueSql("true")
                .HasColumnName("available");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DefaultTax)
                .HasDefaultValueSql("false")
                .HasColumnName("default_tax");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsFavourite).HasColumnName("is_favourite");
            entity.Property(e => e.IteamImg)
                .HasMaxLength(255)
                .HasColumnName("iteam_img");
            entity.Property(e => e.IteamName)
                .HasMaxLength(20)
                .HasColumnName("iteam_name");
            entity.Property(e => e.ItemType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'veg'::character varying")
                .HasColumnName("item_type");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.ShortCode).HasColumnName("short_code");
            entity.Property(e => e.TaxPercentage).HasColumnName("tax_percentage");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .HasDefaultValueSql("'g'::character varying")
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_items_category_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("menu_items_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MenuItemUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("menu_items_updated_by_fkey");
        });

        modelBuilder.Entity<Modifier>(entity =>
        {
            entity.HasKey(e => e.ModifierId).HasName("modifiers_pkey");

            entity.ToTable("modifiers");

            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsAvail)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("is_avail");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModName)
                .HasMaxLength(20)
                .HasColumnName("mod_name");
            entity.Property(e => e.ModifierDesc)
                .HasMaxLength(255)
                .HasColumnName("modifier_desc");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .HasDefaultValueSql("'g'::character varying")
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModifierCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("modifiers_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ModifierUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("modifiers_updated_by_fkey");
        });

        modelBuilder.Entity<ModifierGroup>(entity =>
        {
            entity.HasKey(e => e.ModGroupId).HasName("modifier_groups_pkey");

            entity.ToTable("modifier_groups");

            entity.Property(e => e.ModGroupId).HasColumnName("mod_group_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.GroupDescription)
                .HasMaxLength(255)
                .HasColumnName("group_description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifierName)
                .HasMaxLength(20)
                .HasColumnName("modifier_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModifierGroupCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("modifier_groups_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ModifierGroupUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("modifier_groups_updated_by_fkey");
        });

        modelBuilder.Entity<ModifierGroupMapping>(entity =>
        {
            entity.HasKey(e => e.MappingId).HasName("modifier_group_mapping_pkey");

            entity.ToTable("modifier_group_mapping");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.ModifierGroupId).HasColumnName("modifier_group_id");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");

            entity.HasOne(d => d.ModifierGroup).WithMany(p => p.ModifierGroupMappings)
                .HasForeignKey(d => d.ModifierGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_group_mapping_modifier_group_id_fkey");

            entity.HasOne(d => d.Modifier).WithMany(p => p.ModifierGroupMappings)
                .HasForeignKey(d => d.ModifierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_group_mapping_modifier_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.NoOfPpl).HasColumnName("no_of_ppl");
            entity.Property(e => e.OdrComment)
                .HasMaxLength(255)
                .HasColumnName("odr_comment");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(10)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.OrderType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'dinein'::character varying")
                .HasColumnName("order_type");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("orders_created_by_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_customer_id_fkey");

            entity.HasOne(d => d.Review).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("orders_review_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OrderUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("orders_updated_by_fkey");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("order_details_pkey");

            entity.ToTable("order_details");

            entity.Property(e => e.OrderDetailsId).HasColumnName("order_details_id");
            entity.Property(e => e.Instructions)
                .HasMaxLength(255)
                .HasColumnName("instructions");
            entity.Property(e => e.IteamStatus)
                .HasMaxLength(10)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("iteam_status");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Prepared).HasColumnName("prepared");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_details_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_details_order_id_fkey");
        });

        modelBuilder.Entity<OrderItemModifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_item_modifier_pkey");

            entity.ToTable("order_item_modifier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.OrderDetailsId).HasColumnName("order_details_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.OrderItemModifiers)
                .HasForeignKey(d => d.ModifierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_item_modifier_modifier_id_fkey");

            entity.HasOne(d => d.OrderDetails).WithMany(p => p.OrderItemModifiers)
                .HasForeignKey(d => d.OrderDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_modifier_order_details_id_fkey");
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.OrderTableId).HasName("order_table_pkey");

            entity.ToTable("order_table");

            entity.Property(e => e.OrderTableId).HasColumnName("order_table_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_table_order_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_table_table_id_fkey");
        });

        modelBuilder.Entity<OrderTax>(entity =>
        {
            entity.HasKey(e => e.OrderTaxId).HasName("order_tax_pkey");

            entity.ToTable("order_tax");

            entity.Property(e => e.OrderTaxId).HasColumnName("order_tax_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderTaxes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_tax_order_id_fkey");

            entity.HasOne(d => d.Tax).WithMany(p => p.OrderTaxes)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_tax_tax_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PayId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PayId).HasColumnName("pay_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderTotal).HasColumnName("order_total");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .HasDefaultValueSql("'cash'::character varying")
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_customer_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_order_id_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.CanDelete).HasColumnName("can_delete");
            entity.Property(e => e.CanEdit)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("can_edit");
            entity.Property(e => e.CanView)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("can_view");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.PermissionTypeId).HasColumnName("permission_type_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PermissionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("permissions_created_by_fkey");

            entity.HasOne(d => d.PermissionType).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permissions_permission_type_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permissions_role_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PermissionUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("permissions_updated_by_fkey");
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.HasKey(e => e.PermisssionTypeId).HasName("permission_type_pkey");

            entity.ToTable("permission_type");

            entity.Property(e => e.PermisssionTypeId).HasColumnName("permisssion_type_id");
            entity.Property(e => e.PermissionType1)
                .HasMaxLength(20)
                .HasColumnName("permission_type");
        });

        modelBuilder.Entity<ResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reset_token_pkey");

            entity.ToTable("reset_token");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Used).HasColumnName("used");
            entity.Property(e => e.ValidUpto).HasColumnName("valid_upto");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ambience).HasColumnName("ambience");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Descript)
                .HasColumnType("character varying")
                .HasColumnName("descript");
            entity.Property(e => e.Food).HasColumnName("food");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ServiceRatings).HasColumnName("service_ratings");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_customer_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_order_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("sections_pkey");

            entity.ToTable("sections");

            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.SecName)
                .HasMaxLength(15)
                .HasColumnName("sec_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SectionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("sections_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SectionUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("sections_updated_by_fkey");
        });

        modelBuilder.Entity<TableDetail>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("table_details_pkey");

            entity.ToTable("table_details");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Capacity)
                .HasDefaultValueSql("2")
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.TableStatus)
                .HasMaxLength(10)
                .HasDefaultValueSql("'available'::character varying")
                .HasColumnName("table_status");
            entity.Property(e => e.TblName)
                .HasMaxLength(15)
                .HasColumnName("tbl_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TableDetailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("table_details_created_by_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.TableDetails)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("table_details_section_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TableDetailUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("table_details_updated_by_fkey");
        });

        modelBuilder.Entity<Taxis>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("taxes_pkey");

            entity.ToTable("taxes");

            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsEnabled).HasColumnName("is_enabled");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.TaxAmount).HasColumnName("tax_amount");
            entity.Property(e => e.TaxName)
                .HasMaxLength(10)
                .HasColumnName("tax_name");
            entity.Property(e => e.TaxType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'percentage'::character varying")
                .HasColumnName("tax_type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaxisCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("taxes_created_by_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TaxisModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("taxes_modified_by_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNo, "users_phone_no_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasColumnName("country");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(15)
                .HasColumnName("fname");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Lname)
                .HasMaxLength(15)
                .HasColumnName("lname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .HasColumnName("phone_no");
            entity.Property(e => e.ProfileImg)
                .HasMaxLength(255)
                .HasColumnName("profile_img");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasMaxLength(15)
                .HasColumnName("state");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("users_created_by_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("users_updated_by_fkey");
        });

        modelBuilder.Entity<Waitlist>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("waitlists_pkey");

            entity.ToTable("waitlists");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustEmail)
                .HasMaxLength(20)
                .HasColumnName("cust_email");
            entity.Property(e => e.CustName)
                .HasMaxLength(20)
                .HasColumnName("cust_name");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.NoPeople).HasColumnName("no_people");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.SectionsId).HasColumnName("sections_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WaitlistCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("waitlists_created_by_fkey");

            entity.HasOne(d => d.Sections).WithMany(p => p.Waitlists)
                .HasForeignKey(d => d.SectionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitlists_sections_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WaitlistUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("waitlists_updated_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
