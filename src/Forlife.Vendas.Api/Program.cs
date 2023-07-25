using Amazon;
using Amazon.DynamoDBv2;
using Forlife.Vendas.Data.Repositories;
using Forlife.Vendas.Domain.Handlers.Clientes;
using Forlife.Vendas.Domain.Handlers.LocaisVenda;
using Forlife.Vendas.Domain.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ForlifeVendas", Version = "v1" });
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(CadastrarClienteRequestHandler)));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ConsultarClienteRequestHandler)));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(CadastrarLocalRequestHandler)));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ConsultarLocalRequestHandler)));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(GetLocaisRequestHandler)));
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
builder.Services.AddSingleton<IClienteRepository>(provider => new ClienteRepository(provider.GetRequiredService<IAmazonDynamoDB>()));
builder.Services.AddSingleton<ILocalVendaRepository>(provider => new LocalVendaRepository(provider.GetRequiredService<IAmazonDynamoDB>()));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForlifeVendas v1"));

app.Run();
