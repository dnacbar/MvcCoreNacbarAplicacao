using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using MvcCoreNacbarAplicacao.Services;

namespace MvcCoreNacbarAplicacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBNACBARContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DBNACBARContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Index");

            services.Configure<IdentityOptions>(op =>
            {
                // Password settings
                op.Password.RequireDigit = true;
                op.Password.RequiredLength = 7;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = true;
                op.Password.RequireLowercase = false;
                op.Password.RequiredUniqueChars = 6;

                //Lockout settings
                op.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(30);
                op.Lockout.MaxFailedAccessAttempts = 5;
                op.Lockout.AllowedForNewUsers = true;

                //User settings
                op.User.RequireUniqueEmail = false;
            });

            services.AddMvc().AddRazorPagesOptions(op =>
            {
                op.Conventions.AuthorizeFolder("/Account");
                op.Conventions.AllowAnonymousToPage("/Account/Login");
                op.Conventions.AllowAnonymousToPage("/Account/Register");

                op.Conventions.AuthorizeFolder("/PageAgenda");
                op.Conventions.AuthorizeFolder("/PageBasico");
                op.Conventions.AuthorizeFolder("/PageFinanceiro");
                op.Conventions.AuthorizeFolder("/PageHorario");
                op.Conventions.AuthorizeFolder("/PagePessoa");
                op.Conventions.AuthorizeFolder("/PageUF");
            });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
