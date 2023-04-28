using ARH.Front.Contracts;
using ARH.Front.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DataContext, SqliteDataContext>();
}
else
{
    builder.Services.AddDbContext<DataContext>();
}

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(options => {
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICalendarDalService, CalendarDalService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }  
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
