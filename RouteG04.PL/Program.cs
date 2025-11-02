using Microsoft.EntityFrameworkCore;
using RouteG04.BLL;
using RouteG04.BLL.Services.Classes;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Repositories.Classes;
using RouteG04.DAL.Repositories.Interfaces;

namespace RouteG04.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Configure Services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))    ;
            });
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //Register To Services In DI Container
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddAutoMapper(E => E.AddProfile(new MappingProfiles()));
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
