using AVN.Automapper;
using AVN.Controllers;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Model.Initializer;
using AVN.Utility;
using AVN.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("DefaultConnection"));

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GeneralProfile>());
builder.Services.AddScoped<IMapper, EntityMapper>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<StudentController>();
builder.Services.AddScoped<GroupController>();
builder.Services.AddScoped<GradeBookController>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStatusCodePagesWithReExecute("/Error/NotFound/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

SeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
