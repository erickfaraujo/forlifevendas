namespace Forlife.Vendas.Domain.Exceptions;

public class AtualizarClienteException : BaseException
{
    public AtualizarClienteException() : base("Ocorreu erro ao atualizar o cliente")
    {
    }
}
