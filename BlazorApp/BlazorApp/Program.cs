using BlazorApp.Controller;
using BlazorApp.Data;
using Blazored.Modal;
using DataLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<Identification>();
builder.Services.AddBlazoredModal();

builder.Services.AddSingleton<PersonalSpaceService>();
builder.Services.AddSingleton<AdministrationSpaceService>();
builder.Services.AddSingleton<Identification>();
builder.Services.AddScoped<SignUp>();
builder.Services.AddSingleton<IDataAccess, DataAccess>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Default/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/Default/_Host");

app.Run();
