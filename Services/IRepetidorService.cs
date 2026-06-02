namespace Repetidor.Services;

public interface IRepetidorService
{
    string GerarRepetido(string frase, int vezes, string separador, bool numerar);
    IAsyncEnumerable<string> GerarComStreamAsync(string frase, int vezes, string separador, bool numerar, CancellationToken cancellationToken = default);
}