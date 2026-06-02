using System.Text;

namespace Repetidor.Pages
{
    public partial class Repeater
    {
        private string frase = "";
        private int vezes = 10;
        private string separador = "\n";
        private bool numerar = false;
        private string resultadoParcial = "";
        private string resultadoFinal = "";
        private bool gerando = false;
        private CancellationTokenSource? cts;

        private async Task GerarAsync()
        {
            if (string.IsNullOrWhiteSpace(frase)) return;
            gerando = true;
            resultadoParcial = "";
            resultadoFinal = "";
            StateHasChanged();

            cts = new CancellationTokenSource();
            var token = cts.Token;

            try
            {
                var sb = new StringBuilder();
                int limiteStream = 500;
                bool usarStream = vezes > limiteStream;

                if (!usarStream)
                {
                    resultadoFinal = RepetidorService.GerarRepetido(frase, vezes, separador, numerar);
                    resultadoParcial = resultadoFinal;
                }
                else
                {
                    await foreach (var chunk in RepetidorService.GerarComStreamAsync(frase, vezes, separador, numerar, token))
                    {
                        sb.Append(chunk);
                        resultadoParcial = sb.ToString();
                        StateHasChanged();
                        await Task.Delay(1);
                    }
                    resultadoFinal = sb.ToString();
                    resultadoParcial = resultadoFinal;
                }
            }
            catch (OperationCanceledException)
            {
                resultadoParcial = "Geração cancelada.";
            }
            finally
            {
                gerando = false;
                StateHasChanged();
            }
        }

        private void Limpar()
        {
            cts?.Cancel();
            frase = "";
            vezes = 10;
            resultadoParcial = "";
            resultadoFinal = "";
            gerando = false;
            StateHasChanged();
        }

        public void Dispose() => cts?.Cancel();
    }
}
