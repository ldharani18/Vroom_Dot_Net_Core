using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vroom.AppDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<VroomDBContext>(options =>
    options.UseSqlServer(connectionstring));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    //.AddEntityFrameworkStores<VroomDBContext>();
builder.Services.AddDbContext<VroomDBContext>(options => options.UseSqlServer(connectionstring));
builder.Services.AddMvc(options=>options.EnableEndpointRouting=false);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<VroomDBContext>().AddDefaultUI().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<VroomDBContext, VroomDBContext>();
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

app.UseRouting();
app.UseMvc();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>endpoints.MapRazorPages());
//app.MapControllerRoute(
//   name:"Make",
//   pattern:"{controller=Make}/{action=ByYearMonth}/{year}/{month}");


//app.MapControllerRoute(
//    "ByYearMonth",
//    "make/ByYearMonth/{year:int:length(4)}/{month:int:range(1,12)}",
//    new { Controller = "Make", Action = "ByYearMonth" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
