using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using MGAnonymized.Web.Data.Repositories;
using MGAnonymized.Web.Infrastructure;
using MGAnonymized.Web.Infrastructure.Extensions;
using MGAnonymized.Web.Services;
using MGAnonymized.Web.Services.Interfaces;
using System.IO;

namespace MGAnonymized.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mgAnonymizedOptions = Configuration.GetSection("mgAnonymizedOptions");
            services.Configure<MGAnonymizedOptions>(mgAnonymizedOptions);

            services.AddTransient<IDataInitializerService, DataInitializerService>();
            services.AddTransient<IDownloadService, DownloadService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieRepository, MovieRepository>();


            services.AddStartupTask<DataInitializerStartupFilter>();

            services.AddMemoryCache();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
