using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using System.Net;
using System.Text.Json;

namespace Forlife.Vendas.Data.Repositories;

public class ForlifeVendasRepository : IForlifeVendasRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _vendasTableName = "forlifevendasdb";
    private readonly string _locaisTableName = "LocalVenda";
    private readonly string _idLocalIndex = "idlocal-index";
    private readonly string _skPkIndex = "sk-pk-index";

    public ForlifeVendasRepository(IAmazonDynamoDB dynamoDB) => _dynamoDB = dynamoDB;

    public async Task<bool> CreateAsync<T>(T obj) where T : class
    {
        var objetoJson = JsonSerializer.Serialize(obj);
        var document = Document.FromJson(objetoJson);
        var attributes = document.ToAttributeMap();
        var createRequest = new PutItemRequest() { TableName = _vendasTableName, Item = attributes };

        var response = await _dynamoDB.PutItemAsync(createRequest);

        return response.HttpStatusCode is HttpStatusCode.OK;
    }

    public async Task<T?> GetAsync<T>(string pk, string sk) where T : class
    {
        var getRequest = new GetItemRequest()
        {
            TableName = _vendasTableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "sk", new AttributeValue() { S = sk } },
                { "pk", new AttributeValue() { S = pk } }
            },
            ConsistentRead = true
        };

        var response = await _dynamoDB.GetItemAsync(getRequest);
        
        if (response.Item.Count is 0)
            return null;

        var document = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<T>(document.ToJson());
    }

    public async Task<bool> DeleteAsync<T>(string pk, string sk) where T : class
    {
        var tableName = sk is "PERFIL" ? _vendasTableName : _locaisTableName;

        var attr = new Dictionary<string, AttributeValue>()
        {
            { "pk", new AttributeValue() { S = pk } },
            { "sk", new AttributeValue() { S = sk } }

        };

        var response = await _dynamoDB.DeleteItemAsync(tableName, attr);

        return response.HttpStatusCode is HttpStatusCode.OK;
    }

    public async Task<Pedido?> GetPedidoByIdAsync(string sk)
    {
        var request = new QueryRequest()
        {
            TableName = _vendasTableName,
            IndexName = _skPkIndex,
            ScanIndexForward = true,
            Select = "ALL_PROJECTED_ATTRIBUTES",
            KeyConditionExpression = "sk = :v_sk",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                { ":v_sk", new() { S = $"PEDIDO#{sk}" } }
            },
        };

        var response = await _dynamoDB.QueryAsync(request);

        if (response is null)
            return default;

        var document = Document.FromAttributeMap(response.Items.FirstOrDefault());

        return JsonSerializer.Deserialize<Pedido>(document.ToJson());
    }

    public async Task<IEnumerable<Cliente>?> GetClienteByIdLocalAsync(Guid Id)
    {
        var request = new QueryRequest()
        {
            TableName = _vendasTableName,
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
            TableName = _vendasTableName,
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
            TableName = _vendasTableName,
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
            TableName = _vendasTableName,
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

    public async Task<List<Pedido>?> GetPedidosAsync(ConsultarPedidosRequest parametros)
    {
        var filterExp = "begins_with(sk, :v_sk)";
        var expAtributeValues = new Dictionary<string, AttributeValue>()
        {
            { ":v_sk", new() { S = "PEDIDO"} }
        };

        if (parametros.DataInicio is not null)
        {
            filterExp += " AND datapedido >= :v_dataInicio";
            expAtributeValues.Add(":v_dataInicio", new() { S = parametros.DataInicio });
        }

        if (parametros.DataFim is not null)
        {
            filterExp += " AND datapedido <= :v_dataFim";
            expAtributeValues.Add(":v_dataFim", new() { S = parametros.DataFim });
        }

        if (parametros.StatusPagamento is not null)
        {
            filterExp += " AND statusPagamento = :v_pago";
            expAtributeValues.Add(":v_pago", new() { S = parametros.StatusPagamento });
        }

        if(parametros.IdLocal is not null)
        {
            filterExp += " AND idLocal = :v_local";
            expAtributeValues.Add(":v_local", new() { S = parametros.IdLocal });
        }

        var scanRequest = new ScanRequest()
        {
            TableName = _vendasTableName
        };

        if (filterExp.Length > 1)
        {
            scanRequest.FilterExpression = filterExp;
            scanRequest.ExpressionAttributeValues = expAtributeValues;
        }
            

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
