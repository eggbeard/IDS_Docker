using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vendor.API
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
      services.AddMvc();

      services
        .AddAuthentication(GetAuthenticationOptions)
        .AddJwtBearer(GetJwtBearerOptions);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app
        .UseAuthentication()
        .UseMvc();
    }

    private static void GetAuthenticationOptions(AuthenticationOptions options)
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    private static void GetJwtBearerOptions(JwtBearerOptions options)
    {
      options.Authority = "http://localhost:5000";
      options.Audience = "http://localhost:5000/resources";
      options.RequireHttpsMetadata = false;
    }
  }
}
