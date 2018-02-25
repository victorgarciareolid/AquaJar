using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Aquajar.Cola;
using Aquajar.Solicitudes;
using Aquajar.Parallel;
using System.Runtime.InteropServices;

namespace Aquajar
{
    public class Startup
    {
        public static Recibir r;
        public static Enviar e;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaut", "{controller=Home}/{action=Index}/{id?}");
            });

            r = new Recibir();
            e = new Enviar(r.recibir);
            Task tarea1 = new Task(new Action(e.run));
            Task tarea2 = new Task(new Action(r.run));
            tarea1.Start();
            tarea2.Start();
        }
    }
}
