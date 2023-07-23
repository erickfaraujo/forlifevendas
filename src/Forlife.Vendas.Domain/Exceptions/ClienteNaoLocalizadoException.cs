namespace Forlife.Vendas.Domain.Exceptions;

public class ClienteNaoLocalizadoException : BaseException
{
    public ClienteNaoLocalizadoException() : base("Cliente não localizado")
    {
    }
}
