﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnnetKatan
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
      services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

      // Add framework services.
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
        app.UseExceptionHandler("/Error/Internal");
      }

      app.UseStatusCodePagesWithRedirects("/Error/PageNotFound");

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default_full",
          template: "{controller}/{action}",
          defaults: new { controller = "Home", action = "Index" });

        routes.MapRoute(
          name: "default_simple",
          template: "{action}",
          defaults: new { controller = "Home" });

        routes.MapRoute(
          name: "portfolio_full",
          template: "{controller}/{action}",
          defaults: new { controller = "Portfolio", action = "Index" });
      });
    }
  }
}