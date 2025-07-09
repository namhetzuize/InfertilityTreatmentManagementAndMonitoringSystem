using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<InfertilityTreatmentDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<MedicalRecordService>();
builder.Services.AddScoped<PatientRequestService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<TreatmentServiceService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages(); 
});
