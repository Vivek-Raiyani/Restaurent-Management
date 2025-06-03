using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Implementation;
using pizzashop.repository.Implementations;
using pizzashop.repository.Implementations.Customers;
using pizzashop.repository.Implementations.OrderApp;
using pizzashop.repository.Implementations.Orders;
using pizzashop.repository.Implementations.TableSection;
using pizzashop.repository.Implementations.Tax;
using pizzashop.repository.Interface;
using pizzashop.repository.Interfaces;
using pizzashop.repository.Interfaces.Customers;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.repository.Interfaces.TableSection;
using pizzashop.repository.Interfaces.Tax;
using pizzashop.service.Implementation;
using pizzashop.service.Interface;
using pizzashop.services.Implementations;
using pizzashop.services.Implementations.Customers;
using pizzashop.services.Implementations.OrderApp;
using pizzashop.services.Implementations.Orders;
using pizzashop.services.Implementations.TableSection;
using pizzashop.services.Implementations.Taxes;
using pizzashop.services.Interfaces;
using pizzashop.services.Interfaces.Customers;
using pizzashop.services.Interfaces.OrderApp;
using pizzashop.services.Interfaces.Orders;
using pizzashop.services.Interfaces.TableSection;
using pizzashop.services.Interfaces.Taxes;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();// acces http context everywhere

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PizzashopContext>(
        options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<PermissionFilter>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IPasswordHash,PasswordHash>();
builder.Services.AddScoped<IpermissionServices, PermissionServices>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMenuService, MenuService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IModifierGroupRepository,ModifierGroupRepository>();
builder.Services.AddScoped<IItemModifierGroupMappingRepository,ItemModifierGroupMappingRepository>();
builder.Services.AddScoped<IModifiersRepository, ModifiersRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository , PermissionTypeRepository>();
builder.Services.AddScoped<IModifierGroupMappingRepository, ModifierGroupMappingRepository>();


builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<ISectionServices,SectionServices>();
builder.Services.AddScoped<ITableRepository, TableRepositroy>();
builder.Services.AddScoped<ITableServics, TableServices>();

builder.Services.AddScoped<ITaxRepository, TaxRepositroy>();
builder.Services.AddScoped<ITaxServices,TaxServices>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepositry>();
builder.Services.AddScoped<ICustomerService , CustomerService>();

builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

// builder.Services.AddScoped<IKotRepository, KotRepository>();
builder.Services.AddScoped<IKotServices , KotServices>();

builder.Services.AddScoped<IOrderTableRepository, OrderTableRepository>();
builder.Services.AddScoped<IOrderTableServices , OrderTableServices>();

// builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderMenuServices , OrderMenuServices>();

builder.Services.AddScoped<IWaitingListRepository, WaitingListRepository>();
builder.Services.AddScoped<IWaitingListServices , WaitingListServices>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOrderTaxRepository, OrderTaxRepository>();

builder.Services.AddScoped<IResetTokenReposiotry,ResetTokenReposiotry>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventServices, EventServices>();


// Add authentication using cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Index"; // Redirect to login if not authenticated
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect if unauthorized
    });

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(300); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Auth/Error");

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// app.UseStatusCodePagesWithReExecute("/Home/Error", "?errorCode={0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
