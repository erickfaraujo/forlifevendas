namespace Forlife.Vendas.Domain.Exceptions;

public class CadastrarLocalException : BaseException
{
    public CadastrarLocalException() : base("Ocorreu erro ao cadastrar o local")
    {
    }
}
