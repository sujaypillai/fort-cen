using fort_cen.Data;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Fast.Components.FluentUI;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();

builder.Services.AddSingleton<WeatherForecastService>();

WebApplication? app = builder.Build();
app.UsePathBase("/cen");
app.MapBlazorHub();
// app.Map("/cen",cen => {
//     cen.UsePathBase("/cen");
//     cen.UseRouting();
//     cen.UseEndpoints(endpoints => endpoints.MapBlazorHub());
// });
app.Environment.WebRootPath = "cen";

if (app.Environment.IsDevelopment())
{
builder.WebHost.UseStaticWebAssets();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var rewriteOptions = new RewriteOptions();
    rewriteOptions.AddRewrite("_blazor/initializers", "/_blazor/initializers", skipRemainingRules: true);
    
app.UseRewriter(rewriteOptions);
app.MapRazorPages();
// app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
