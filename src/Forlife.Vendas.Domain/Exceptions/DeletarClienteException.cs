namespace Forlife.Vendas.Domain.Exceptions;

public class DeletarClienteException : BaseException
{
    public DeletarClienteException() : base("Não foi possível deletar o cliente solicitado!")
    {
    }
}
