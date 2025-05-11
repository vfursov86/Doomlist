using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite; // This might be needed


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DoomlistContext>(options =>
    options.UseSqlite($"Data Source=Database/doomlistdata.db"));

builder.Services.AddScoped<AlbumService>();


var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
