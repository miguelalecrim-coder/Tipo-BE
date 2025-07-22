using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Application.Services;
using Domain.IRepository;
using Infrastructure.Repositories;
using Domain.Factory;
using Infrastructure.Resolvers;
using Domain.Models;
using Application.DTO;
using MassTransit;
using Application.IPublishers;
using WebApi.Publishers;
using Domain.Messages;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<ITipoService, TipoService>();
builder.Services.AddScoped<IMessagePublisher, MassTransitPublisher>();

//Repositories
builder.Services.AddTransient<ITipoRepository, TipoRepositoryEF>();

//Factories
builder.Services.AddTransient<ITipoFactory, TipoFactory>();

//Mappers

builder.Services.AddTransient<TipoDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<Tipo, TipoDTO>();
});

//MassTransit
var InstanceId = InstanceInfo.InstanceId;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TipoCreatedConsumer>();
    x.AddConsumer<EdicaoWithoutTipoCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint($"tipos-cmd-{InstanceId}", conf =>
        {
            conf.ConfigureConsumer<TipoCreatedConsumer>(context);

        });

        cfg.ReceiveEndpoint("tipos-cmd-saga", conf =>
        {
            conf.ConfigureConsumer<EdicaoWithoutTipoCreatedConsumer>(context);
        });

    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// read env variables for connection string
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.

var env = app.Environment.EnvironmentName;

if (env == "Development" || env.StartsWith("Instance"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AbsanteeContext>();
    dbContext.Database.Migrate();
}

app.Run();

public partial class Program { }
