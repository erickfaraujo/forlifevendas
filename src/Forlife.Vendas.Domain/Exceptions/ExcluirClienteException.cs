namespace Forlife.Vendas.Domain.Exceptions;

public class ExcluirClienteException : BaseException
{
    public ExcluirClienteException() : base("Ocorreu erro ao excluir o cliente")
    {
    }
}
