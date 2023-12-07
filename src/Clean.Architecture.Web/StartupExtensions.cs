using Ardalis.ListStartupServices;
using Clean.Architecture.Web.ViewServices;
using FastEndpoints;
using FastEndpoints.ApiExplorer;
using FastEndpoints.Swagger.Swashbuckle;
using Microsoft.OpenApi.Models;
using Clean.Architecture.Infrastructure;
using Clean.Architecture.Infrastructure.Data;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Autofac;
using Clean.Architecture.Core;
using Clean.Architecture.Web.Middlewares;

namespace Clean.Architecture.Web;

public static class StartupExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
      options.CheckConsentNeeded = context => true;
      options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
    builder.Services.AddDbContext(connectionString!);

    builder.Services.AddControllersWithViews().AddNewtonsoftJson();
    builder.Services.AddRazorPages();
    builder.Services.AddFastEndpoints();
    builder.Services.AddFastEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      c.EnableAnnotations();
      c.OperationFilter<FastEndpointsOperationFilter>();
    });
    
    builder.Services.AddTransient<IOrderService, OrderService>();

    builder.Services.Configure<ServiceConfig>(config =>
    {
      config.Services = new List<ServiceDescriptor>(builder.Services);

      config.Path = "/listservices";
    });

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
      containerBuilder.RegisterModule(new DefaultCoreModule());
      containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
    });

    return builder.Build();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseShowAllServicesMiddleware();
    }
    else
    {
      app.UseExceptionHandler("/Home/Error");
      app.UseHsts();
    }
    app.UseRouting();
    app.UseFastEndpoints();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

    app.MapDefaultControllerRoute();
    app.MapRazorPages();

    app.UseCustomExceptionHandler();

    return app;
  }

  public static void CreateDatabase(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
      var context = services.GetRequiredService<AppDbContext>();
      context.Database.EnsureCreated();
      SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
      var logger = services.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
  }
}
