using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Infrastructure.Settings;
using EDeals.Catalog.Infrastructure;
using Serilog;
using EDeals.Catalog.Infrastructure.Shared.Middlewares;
using EDeals.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using EDeals.Catalog.Application;
using EDeals.Catalog.Infrastructure.Seeders;
using Stripe;
using EDeals.Catalog.Application.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application...");

    var builder = WebApplication.CreateBuilder(args);

    // Add configurations
    ApiExtensions.AddApplicationSettings(builder);
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add Services
    var dbSettings = builder.Configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();
    builder.Services.AddDbContext(dbSettings!);

    builder.Services
        .AddApplicationMethods()
        .AddInfrastructureMethods()
        .AddApplicationConfigureSettings(builder.Configuration)
        .AddInfraConfigureSettings(builder.Configuration);

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    // Adaugarea serviciului
    builder.Services.AddSignalR();

    // Add stripe
    var stripeSettings = builder.Configuration.GetSection(nameof(StripeSettings)).Get<StripeSettings>();
    StripeConfiguration.ApiKey = stripeSettings.ApiKey;

    builder.Services.AddAuthorization(opt =>
    {
        opt.AddPolicy("User", policy => policy.RequireRole("User").RequireAuthenticatedUser().Build());
        opt.AddPolicy("Admin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser().Build());
        opt.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin").RequireAuthenticatedUser().Build());
    });

    // Add Logging
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    RunMigrations(app);
    await RunSeeders(app);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(corsOpt => {
        corsOpt.SetIsOriginAllowed(x => true)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });

    app.UseRouting();

    app.UseAuthorization();
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<ChatHub>("/chat");

        endpoints.MapControllers();
    });

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();


    app.Run();
}
catch (Exception ex) when (ex is not OperationCanceledException && !ex.GetType().Name.Contains("StopTheHostException") && !ex.GetType().Name.Contains("HostAbortedException"))
{
    Log.Fatal(ex, "Unhandled exception in Program");
}
finally
{
    Log.Information("App shut down complete");
    Log.CloseAndFlush();
}

void RunMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

async Task RunSeeders(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var seeders = scope.ServiceProvider.GetRequiredService<CategoriesSeeder>();
    await seeders.AddTopCategories();
    await seeders.AddSubcategories();
}