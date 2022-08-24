using GarageRelation.Configuration;
using GarageRelation.Controllers.Repositories;
using GarageRelation.Controllers.Services;
using GarageRelation.Utils.HealthChecks;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MySqlRepository>(options =>
{
    MySqlConfiguration config = builder.Configuration.GetSection("MySqlSettings").Get<MySqlConfiguration>();
    ServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0));

    options.UseMySql(config.ConnectionString, serverVersion);
});

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddControllers(config =>
{
    config.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Database Health Check");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
