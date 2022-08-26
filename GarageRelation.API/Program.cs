using GarageRelation.API.Configuration;
using GarageRelation.API.Controllers.Repositories;
using GarageRelation.API.Controllers.Services;
using GarageRelation.API.Utils;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

// ############# WebApplication configuration before building. ###############
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Get the database configuration Object
MySqlConfiguration mySqlConfiguration = builder.Configuration.GetSection("MySqlSettings").Get<MySqlConfiguration>();

// ############# Add services to the container. ###############

// Add the DB Context using Mysql 8
builder.Services.AddDbContext<MySqlRepository>(options =>
{
	MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0));
	options.UseMySql(mySqlConfiguration.ConnectionString, serverVersion);
});

// Add common service dependencies
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICarService, CarService>();

// Add controllers
builder.Services.AddControllers(config =>
{
	config.SuppressAsyncSuffixInActionNames = false;
});

// Add the swagger services
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the healthChecks
builder.Services.AddHealthChecks().AddDbContextCheck<MySqlRepository>(tags: new[] { "ready" });

// ############# Build the WebApplication #############
WebApplication app = builder.Build();

// ############# Configure the HTTP request pipeline. AKA WebApplication config after building #############

// Add swaggerUi only in development mode
// HTTPS redirection is automatic in production environment
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseHttpsRedirection();
}

app.UseAuthorization();

// Use the controllers Route
app.MapControllers();

// Use the ready health check route with custom response
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
	Predicate = (check) => check.Tags.Contains("ready"),
	ResponseWriter = Utilities.GenerateHealthCheckCustomResponse
});

// Use the live health check route with no custom health checks (only checks if the server gives a response)
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
	Predicate = (_) => false
});

// ############# Runs the app ###############
app.Run();
