using Cartend.Api.Dtos;
using Cartend.Api.Endpoints;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddCors(
    x => x.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

Cartend.Api.DataAccess.DependencyInjection.RegisterServices(builder.Services, builder.Environment);
Cartend.Api.Logic.DependencyInjection.RegisterServices(builder.Services);

var app = builder.Build();

app.UseCors();

EndpointMapper.MapAll(app);

app.Run();
