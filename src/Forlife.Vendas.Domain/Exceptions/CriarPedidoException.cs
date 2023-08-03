namespace Forlife.Vendas.Domain.Exceptions;

public class CriarPedidoException : BaseException
{
    public CriarPedidoException() : base("Ocorreu um erro ao criar o pedido")
    {
    }
}
