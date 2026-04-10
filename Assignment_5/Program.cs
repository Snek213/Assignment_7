using Assignment_5.Data;
using Assignment_7.Data;
using Assignment_7.Models;
using Assignment_7.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
// *** HERE ARE OUR NEW LINES ***
builder.Services.AddScoped<IMovieRepo, MovieRepoEf>();
//builder.Services.AddSingleton<IMovieRepo, MovieRepoList>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Assignment_5Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Assignment_5Context") ?? throw new InvalidOperationException("Connection string 'Assignment_5Context' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<Assignment_5Context>();

//

// add this section to configure authorization options
builder.Services.AddAuthorization(options =>
{
    // in our authorization options we add a policy
    // that requires the user to have the admin role
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// add this section to configure options for our razor pages
builder.Services.AddRazorPages(options =>
{
    // secure anything in the Pages/Items folder 
    // by assigning it the admin policy
    // which we created above 
    // saying it requires a user to have the admin role
    options.Conventions.AuthorizeFolder("/Items", "AdminPolicy");
});

//

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await AdminHelper.SeedAdminAsync(scope.ServiceProvider);
SeedData.Initialize(scope.ServiceProvider);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
