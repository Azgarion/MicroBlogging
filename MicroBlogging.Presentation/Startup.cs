using MicroBlogging.Service;
using MicroBlogging.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MicroBlogging
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string CorsOrigin = "AllowAnyOrigin";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MicroBlogDatabaseSettings>(
                Configuration.GetSection(nameof(MicroBlogDatabaseSettings)));

            services.AddSingleton<IMicroBlogDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MicroBlogDatabaseSettings>>().Value);
            services.AddSingleton<ThreadService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy(CorsOrigin, builder => builder.WithOrigins().AllowAnyOrigin().AllowAnyHeader());
                
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(CorsOrigin);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}