using Cartend.Api.Endpoints;
using Cartend.Dtos.V1.Entities;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddCors(
    x => x.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

Cartend.DataAccess.DependencyInjection.RegisterServices(builder.Services, builder.Environment.WebRootPath);
Cartend.Logic.DependencyInjection.RegisterServices(builder.Services);

var app = builder.Build();

app.UseCors();

EndpointMapper.MapAll(app);

app.Run();
