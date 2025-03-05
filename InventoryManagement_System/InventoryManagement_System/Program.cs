using InventoryManagement_System.Business;
using InventoryManagement_System.Helper;
using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVendor, VendorServices>();
builder.Services.AddScoped<IProduct, ProductSevices>();
builder.Services.AddScoped<ICetegory, CetegoryServices>();
builder.Services.AddScoped<ICustomer, CustomerServices>();
builder.Services.AddScoped<IS_Vendor, VendorHelper>();
builder.Services.AddScoped<IS_Customer, CustomerHelper>();
builder.Services.AddScoped<IStock, StockService>();
builder.Services.AddScoped<IS_Stock, StockHelper>();



builder.Services.AddScoped<IAuth , AuthService>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "User";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true; // prevent client side acess
    options.Cookie.IsEssential = true;
});




var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
