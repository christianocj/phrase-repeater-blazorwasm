using System.Text;

namespace Repetidor.Services;

public class RepetidorService : IRepetidorService
{
    public string GerarRepetido(string frase, int vezes, string separador, bool numerar)
    {
        var sb = new StringBuilder();
        for (int i = 1; i <= vezes; i++)
        {
            if (numerar)
                sb.Append($"{i} - ");
            sb.Append(frase);
            if (i < vezes) sb.Append(separador.Replace("\\n", "\n"));
        }
        return sb.ToString();
    }

    public async IAsyncEnumerable<string> GerarComStreamAsync(string frase, int vezes, string separador, bool numerar, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int chunkSize = 50;
        var buffer = new StringBuilder();
        for (int i = 1; i <= vezes; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (numerar)
                buffer.Append($"{i} - ");
            buffer.Append(frase);
            if (i < vezes) buffer.Append(separador.Replace("\\n", "\n"));

            if (i % chunkSize == 0 || i == vezes)
            {
                yield return buffer.ToString();
                buffer.Clear();
                await Task.Yield();
            }
        }
    }
}