using Microsoft.EntityFrameworkCore;
using Skyfri.BL.IServices;
using Skyfri.BL.Services;
using Skyfri.data_access;
using Skyfri.Repository.DataManager;
using Skyfri.Repository.IDataManager;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    o.IncludeXmlComments(xmlPath);
});

///Add DbContext service
builder.Services.AddDbContext<SkyfriDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SkyfriConnection")));

//Add automapper
builder.Services.AddAutoMapper(typeof(Program));

//use repositories
builder.Services.AddTransient<ITenantRepository, TenantManager>();
builder.Services.AddTransient<IPortfolioRepository, PortfolioManager>();
builder.Services.AddTransient<IPlantRepository, PlantManager>();

builder.Services.AddTransient<ITenantService, TenantService>();
builder.Services.AddTransient<IPortfolioService, PortfolioService>();
builder.Services.AddTransient<IPlantService, PlantService>();

//Add CORS for the React-UI pages
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            //Just for developmental use
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

//enable logging
builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowSpecificOrigin");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
