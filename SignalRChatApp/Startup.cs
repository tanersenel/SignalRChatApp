using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using SignalRChatApp.Data;
using SignalRChatApp.Hubs;
using SignalRChatApp.Repositories;
using SignalRChatApp.Repositories.Interfaces;
using SignalRChatApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp
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
            services.Configure<ChatDatabaseSettings>(Configuration.GetSection(nameof(ChatDatabaseSettings)));
            services.AddSingleton<IChatDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ChatDatabaseSettings>>().Value);
            services.AddTransient<IChatContext, ChatContext>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IRedisService, RedisService>();
            services.AddSignalR();

            services.AddControllersWithViews();
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "SignalRChatApp";
                options.Configuration = Configuration.GetSection("ConnectionStringsCache:Redis").Value; 
            });
            services.AddScoped<IRedisClientsManager>(c => new RedisManagerPool(Configuration.GetSection("ConnectionStringsCache:Redis").Value));
            services.AddScoped<IRedisClient>(c => c.GetService<IRedisClientsManager>().GetClient());
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(60));

            services.Configure<ConnectionStringsCache>(Configuration.GetSection("ConnectionStringsCache"));

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
                app.UseExceptionHandler("/Chat/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Chat}/{action=Index}/{id?}");
            });
        }
    }
}
