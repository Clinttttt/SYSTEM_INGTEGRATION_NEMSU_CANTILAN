using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Client.Components;
using SYSTEM_INGTEGRATION_NEMSU.Client.Helper;
using SYSTEM_INGTEGRATION_NEMSU.Client.Services;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.BackGroundHelper;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PageState>();
builder.Services.AddHostedService<InvoiceMonitorService>();


builder.Services.AddHttpClient<IAuthApiServices, AuthApiServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IStudentRecordApiCommand, StudentRecordApiCommand>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IEnrollmentApiServices, EnrollmentApiServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IFacultyRecordApi, FacultyRecordApi>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IHandlingApiCourse, HandlingApiCourse>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IHandlingStudentsApi, HandlingStudentsApi>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IRespondApiCommand, RespondApiCommand>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddHttpClient<IStudentRecordApiCommand, StudentRecordApiCommand>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});
builder.Services.AddScoped<IAuthHelper, AuthHelper>();



builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7072");
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();




app.Run();