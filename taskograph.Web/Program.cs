using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models;

namespace taskograph.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            // try catch for logging purposes
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<TasksContext>(options =>
                    options.UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
                builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TasksContext>();
                builder.Services.AddScoped<ITaskRepository, TaskRepository>();
                builder.Services.AddScoped<IEntryRepository, EntryRepository>();
                builder.Services.AddScoped<IDurationRepository, DurationRepository>();
                builder.Services.AddScoped<IPreciseTargetRepository, PreciseTargetRepository>();
                builder.Services.AddScoped<IRegularTargetRepository, RegularTargetRepository>();
                builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
                builder.Services.AddScoped<IGroupRepository, GroupRepository>();
                builder.Services.AddScoped<IColorRepository, ColorRepository>();
                builder.Services.AddControllersWithViews();
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();
                app.Run();
            }
            catch (Exception e)
            {
                logger.Error($"Program.cs: Message: {e.Message}");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}