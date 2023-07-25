using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using System.Net;
using System.Text.Json;

namespace Forlife.Vendas.Data.Repositories;

public class LocalVendaRepository : ILocalVendaRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _tableName = "LocalVenda";

    public LocalVendaRepository(IAmazonDynamoDB dynamoDB) => _dynamoDB = dynamoDB;

    public async Task<bool> CreateAsync(LocalVenda localVenda)
    {
        var clienteJson = JsonSerializer.Serialize(localVenda);
        var document = Document.FromJson(clienteJson);
        var attributes = document.ToAttributeMap();
        var createRequest = new PutItemRequest() { TableName = _tableName, Item = attributes};

        var response = await _dynamoDB.PutItemAsync(createRequest);

        return response.HttpStatusCode is HttpStatusCode.OK;
    }

    public async Task<LocalVenda?> GetAsync(Guid id)
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

        return JsonSerializer.Deserialize<LocalVenda>(document.ToJson());
    }

    public async Task<IEnumerable<LocalVenda>?> GetAllAsync()
    {
        var scanRequest = new ScanRequest() { TableName = _tableName };

        var response = await _dynamoDB.ScanAsync(scanRequest);
        if (response.ScannedCount is 0)
            return null;

        var locais = new List<LocalVenda>();

        foreach(var item in response.Items)
        {
            var document = Document.FromAttributeMap(item);
            locais.Add(JsonSerializer.Deserialize<LocalVenda>(document.ToJson())!);
        }

        return locais;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDB.DeleteItemAsync(deleteItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
