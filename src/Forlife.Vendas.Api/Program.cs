using Amazon;
using Amazon.DynamoDBv2;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Services.Handlers;
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
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
builder.Services.AddSingleton<IClienteRepository>(provider => new ClienteRepository(provider.GetRequiredService<IAmazonDynamoDB>()));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForlifeVendas v1"));

app.MapGet("/", () => "API ForlifeVendas. Para acessar o swagger, adicione /swagger no final da URL");

app.Run();
