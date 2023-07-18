using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using System.Net;
using System.Text.Json;

namespace Forlife.Vendas.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _tableName = "Clientes";

    public ClienteRepository(IAmazonDynamoDB dynamoDB) => _dynamoDB = dynamoDB;

    public async Task<bool> CreateAsync(Cliente cliente)
    {
        var clienteJson = JsonSerializer.Serialize(cliente);
        var document = Document.FromJson(clienteJson);
        var attributes = document.ToAttributeMap();
        var createRequest = new PutItemRequest() { TableName = _tableName, Item = attributes};

        var response = await _dynamoDB.PutItemAsync(createRequest);

        return response.HttpStatusCode is HttpStatusCode.OK;
    }

    public async Task<Cliente?> GetAsync(Guid id)
    {
        var getRequest = new GetItemRequest()
        { 
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue() { S = id.ToString() } },
                { "sk", new AttributeValue() { S = id.ToString() } }
            },
            ConsistentRead = true
        };

        var response = await _dynamoDB.GetItemAsync(getRequest);
        if (response.Item.Count is 0)
            return null;

        var document = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<Cliente>(document);
    }
}
