using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RpgApi;
using RpgApi.Context;
using RpgApi.Interfaces;
using RpgApi.Repository;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<RPGDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(sw =>
        sw.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "RPG Api",
            Version = "v1",
            Description = "Simple CRUD using Entity Framework 7"
        }));

    var key = Encoding.ASCII.GetBytes(KeyToken.Secret);
    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(op =>
    {
        op.RequireHttpsMetadata = false;
        op.SaveToken = true;
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    builder.Services.AddCors();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(e => e
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}