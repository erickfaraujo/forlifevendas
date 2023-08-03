namespace Forlife.Vendas.Domain.Exceptions;

public class PedidoNaoLocalizadoException : BaseException
{
    public PedidoNaoLocalizadoException() : base("Pedido não encontrado")
    {
    }
}
