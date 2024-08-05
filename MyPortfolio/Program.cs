using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;
using MyPortfolio.Repository;
using MyPortfolio.Repository.IRepository;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyPortfolio.Statics;
using MyPortfolio.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
builder.Services.AddRazorPages(); 
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender,ImplementIEmailSender>();
builder.Services.AddAuthentication().AddFacebook(options => {
    options.AppId = "1171734560523333";
    options.AppSecret = "73fa6c1b346be6d1a718384d3dc89694";
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();

app.UseAuthentication();

seedatabase();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MobileSelling}/{action=DisplayCustomer}/{id?}");

app.Run();
void seedatabase() {
    using (var scope=app.Services.CreateScope()) {
        var dbinitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbinitialize.initialize();
    }

}