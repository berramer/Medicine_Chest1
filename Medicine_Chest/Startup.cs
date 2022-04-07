using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Medicine_Chest.EmailServices;


namespace Medicine_Chest
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
           
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Server=DESKTOP-B28EHI3;Database=Eczane;User Id=sa;Password=berra123;"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true; //parola i�inde say�sal de�er olmal�d�r
                options.Password.RequireLowercase = true; // parola i�inde k���k harf olmak zorunda
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8; // parola min. ka� karakter olaca��
                options.Password.RequireNonAlphanumeric = true; // parola i�inde bir karakter olmal�d�r

                //Locaoutssss
                options.Lockout.MaxFailedAccessAttempts = 5; //parolan�n max 5 defa yanl�� girebilir
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
///user i�inde olmas�n� istedi�iniz karakterler
                //options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true; // ayn� mail adresinden iki kullan�c� olamaz
                options.SignIn.RequireConfirmedEmail = true; ; //kullan�c�ya onay maili g�nderilmesi
                options.SignIn.RequireConfirmedPhoneNumber = false;


            });

            //cookie ayarlar�

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true; //cookie nin ya�am s�resini belirler
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".medicinechest.security.cookie",
                    SameSite = SameSiteMode.Strict
                };

            });
            //email injection
            services.AddScoped<IEmailSender, EMailSender>(i => new EMailSender(
                Configuration["EmailSender:Host"],
                Configuration.GetValue<int>("EmailSender:Port"),
                Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                Configuration["EmailSender:UserName"],
                Configuration["EmailSender:Password"]
                ));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
