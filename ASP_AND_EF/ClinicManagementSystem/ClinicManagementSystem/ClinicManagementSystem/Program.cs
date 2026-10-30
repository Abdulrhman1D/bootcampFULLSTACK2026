using ClinicManagementSystem.Infrastructure.Data;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.Services.Base;
using ClinicManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(conectionString));


// ================================ 
// Cookie Authentication 
// ================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// ================================
// Repositories
// ================================
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository,AppointmentRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

// ================================
// Unit Of Work
// ================================

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ================================
// Services
// ================================
builder.Services.AddScoped<IAppointmentServices,AppointmentServices>();
builder.Services.AddScoped<IDoctorServices,DoctorServices>();
builder.Services.AddScoped<IPatientServices,PatientServices>();
builder.Services.AddScoped<IJobServices,JobServices>();
builder.Services.AddScoped<ISpecialtyServices,SpecialtyServices>();
builder.Services.AddScoped<IUserServices,UserServices>();




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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
