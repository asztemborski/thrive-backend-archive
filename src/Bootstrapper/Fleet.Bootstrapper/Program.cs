using Fleet.Shared.Application;
using Fleet.Shared.Infrastructure;
using Fleet.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration, "Fleet.src.Modules.");
var modules = ModuleLoader.LoadModules(assemblies);

builder.WebHost.ConfigureModules();
builder.Services.AddSharedApplication(assemblies);
builder.Services.AddSharedInfrastructure(builder.Configuration, assemblies, modules);

foreach (var module in modules)
{
    module.Add(builder.Services);
}

builder.Services.AddControllers();

var app = builder.Build();
app.UseSharedInfrastructure();

foreach (var module in modules)
{
    module.Use(app);
}

app.MapControllers();

assemblies.Clear();
modules.Clear();

app.Run();
