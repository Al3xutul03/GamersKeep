using BusinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(cfg =>
            {
                var licenseKey = builder.Configuration["AutoMapper:LicenseKey"];
                if (!string.IsNullOrWhiteSpace(licenseKey))
                {
                    cfg.LicenseKey = licenseKey;
                }

                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });

            // Pool AppDbContext instances rather than allocating one per
            // request. Same scoped semantics from the caller's perspective;
            // EF Core resets the change tracker on return-to-pool.
            // Default pool size is 1024.
            builder.Services.AddDbContextPool<AppDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DbDevConnection"),
                                 ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbDevConnection"))));

            // Redirect DbContext (the base type used by BaseRepository) to the
            // SAME pooled AppDbContext instance within the scope. Without
            // this, resolving DbContext would create a fresh non-pooled
            // AppDbContext and bypass the pool entirely.
            builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

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

            app.Lifetime.ApplicationStarted.Register(async () => {

                // Apply any pending EF Core migrations on startup. This also acts
                // as the database connectivity check (Migrate() opens a connection)
                // and brings the schema up to date with the model automatically.
                // NOTE: this is convenient for a single-instance application.
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                    Console.WriteLine($"[{DateTime.UtcNow.ToLocalTime()}] Database migrated and connection successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.UtcNow.ToLocalTime()}] Database migration failed: {ex.Message}");
                }
            });

            app.Run();
        }
    }
}
