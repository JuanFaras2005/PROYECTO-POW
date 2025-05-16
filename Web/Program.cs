using Application;
using AutoMapper;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Common;

namespace MvcTemplate;

public class Program
{
    public static async Task Main(string[] args) // ahora es async
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var mappingConfiguration = new MapperConfiguration(m => m.AddProfile(new MProfile()));
        IMapper mapper = mappingConfiguration.CreateMapper();
        builder.Services.AddSingleton(mapper);

        // Register infrastructure and services
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddServices(builder.Configuration);

        // Add Identity services with custom ApplicationUser
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Add controllers with views support
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Crear roles automáticamente
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await CrearRolesAsync(services); // <--- llamada al método que vamos a definir abajo
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
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

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Administrador", "Profesor", "Estudiante" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        app.Run();
    }

    public static async Task CrearRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Administrador", "Profesor", "Estudiante" };

        foreach (var rol in roles)
        {
            var existe = await roleManager.RoleExistsAsync(rol);
            if (!existe)
            {
                await roleManager.CreateAsync(new IdentityRole(rol));
            }
        }
    }
}
