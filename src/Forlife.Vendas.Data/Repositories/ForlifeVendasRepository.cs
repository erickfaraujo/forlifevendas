using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using System.Net;
using System.Text.Json;

namespace Forlife.Vendas.Data.Repositories;

public class ForlifeVendasRepository : IForlifeVendasRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _tableName = "forlifevendasdb";
    private readonly string _idLocalIndex = "idlocal-index";
    private readonly string _skPkIndex = "sk-pk-index";

    public ForlifeVendasRepository(IAmazonDynamoDB dynamoDB) => _dynamoDB = dynamoDB;

    public async Task<bool> CreateAsync<T>(T obj) where T : class
    {
        var objetoJson = JsonSerializer.Serialize(obj);
        var document = Document.FromJson(objetoJson);
        var attributes = document.ToAttributeMap();
        var createRequest = new PutItemRequest() { TableName = _tableName, Item = attributes };

        var response = await _dynamoDB.PutItemAsync(createRequest);

        return response.HttpStatusCode is HttpStatusCode.OK;
    }

    public async Task<T?> GetAsync<T>(string pk, string sk) where T : class
    {
        var getRequest = new GetItemRequest()
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue() { S = pk } },
                { "sk", new AttributeValue() { S = sk } }
            },
            ConsistentRead = true
        };

        var response = await _dynamoDB.GetItemAsync(getRequest);
        if (response.Item.Count is 0)
            return null;

        var document = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<T>(document.ToJson());
    }

    public async Task<IEnumerable<Cliente>?> GetClienteByIdLocalAsync(Guid Id)
    {
        var request = new QueryRequest()
        {
            TableName = _tableName,
            IndexName = _idLocalIndex,
            ScanIndexForward = true,
            Select = "ALL_PROJECTED_ATTRIBUTES",
            KeyConditionExpression = "idlocal = :v_idlocal",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                { ":v_idlocal", new() { S = Id.ToString() } }
            },
        };

        var response = await _dynamoDB.QueryAsync(request);

        var listaClientesEncontrados = new List<Cliente>();

        foreach (var cliente in response.Items)
        {
            var document = Document.FromAttributeMap(cliente);
            listaClientesEncontrados.Add(JsonSerializer.Deserialize<Cliente>(document.ToJson())!);
        }

        return listaClientesEncontrados;
    }

    public async Task<IEnumerable<T>?> GetAllAsync<T>() where T : class
    {
        var sk = typeof(T).Name switch
        {
            "Cliente" => "PERFIL",
            "Pedido" => "PEDIDO",
            _ => "PERFIL"
        };

        var request = new QueryRequest()
        {
            TableName = _tableName,
            IndexName = _skPkIndex,
            ScanIndexForward = true,
            Select = "ALL_PROJECTED_ATTRIBUTES",
            KeyConditionExpression = "sk = :v_sk",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                { ":v_sk", new() { S = sk } }
            },
        };

        var response = await _dynamoDB.QueryAsync(request);

        var listaClientesEncontrados = new List<T>();

        foreach (var cliente in response.Items)
        {
            var document = Document.FromAttributeMap(cliente);
            listaClientesEncontrados.Add(JsonSerializer.Deserialize<T>(document.ToJson())!);
        }

        return listaClientesEncontrados;
    }

    public async Task<List<Pedido>?> GetPedidosClienteAsync(string pk)
    {
        var request = new QueryRequest()
        {
            TableName = _tableName,
            ScanIndexForward = true,
            KeyConditionExpression = "pk = :v_pk AND begins_with(sk, :v_sk)",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                { ":v_pk", new() { S = pk} },
                { ":v_sk", new() { S = "PEDIDO" } }
            },
        };

        var response = await _dynamoDB.QueryAsync(request);

        var listaPedidos = new List<Pedido>();

        foreach (var cliente in response.Items)
        {
            var document = Document.FromAttributeMap(cliente);
            listaPedidos.Add(JsonSerializer.Deserialize<Pedido>(document.ToJson())!);
        }

        return listaPedidos;
    }

    public async Task<List<Pedido>?> GetPedidosPorDataAsync(string dataInicio, string dataFim)
    {
        var scanRequest = new ScanRequest()
        {
            TableName = _tableName,
            FilterExpression = "datapedido >= :v_dataInicio AND datapedido <= :v_dataFim",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                { ":v_dataInicio", new() { S = dataInicio } },
                { ":v_dataFim", new() { S = dataFim } }
            }
        };

        var response = await _dynamoDB.ScanAsync(scanRequest);
        if (response.ScannedCount is 0)
            return null;

        var pedidos = new List<Pedido>();

        foreach (var item in response.Items)
        {
            var document = Document.FromAttributeMap(item);
            pedidos.Add(JsonSerializer.Deserialize<Pedido>(document.ToJson())!);
        }

        return pedidos;
    }
}
