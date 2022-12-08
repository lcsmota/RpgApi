using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RpgApi.Context;
using RpgApi.Interfaces;
using RpgApi.Repository;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<RPGDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(sw =>
        sw.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "RPG Api",
            Version = "v1",
            Description = "Simple CRUD using Entity Framework 7"
        }));
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}