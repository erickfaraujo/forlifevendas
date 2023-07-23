namespace Forlife.Vendas.Domain.Exceptions;

public class CadastrarClienteException : BaseException
{
    public CadastrarClienteException() : base("Ocorreu erro ao cadastrar o cliente")
    {
    }
}
