using DentalClinic.app.AppServices.FileUploadService;
using DentalClinic.App.AppServises.Mapper;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using DentalClinic.EF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IDoctorRepository), typeof(DoctorRepository));
builder.Services.AddTransient(typeof(IRoomRepository), typeof(RoomRepository));
builder.Services.AddTransient(typeof(IClinicRepository), typeof(ClinicRepository));
builder.Services.AddTransient(typeof(IPatientRepository), typeof(PatientRepository));
builder.Services.AddTransient(typeof(IAppointmentRepository), typeof(AppointmentRepository));
builder.Services.AddTransient(typeof(IVisitRepository), typeof(VisitRepository));
builder.Services.AddTransient(typeof(ITreatmentRepository), typeof(TreatmentRepository));
builder.Services.AddTransient(typeof(IImageRepository), typeof(ImageRepository));
builder.Services.AddTransient(typeof(IFinanceRepository), typeof(FinanceRepository));
builder.Services.AddTransient(typeof(ILabRepository), typeof(LabRepository));
builder.Services.AddTransient(typeof(IImageService), typeof(ImageService));


//Auto Mapper.
builder.Services.AddAutoMapper(typeof(MappingProfile));


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
