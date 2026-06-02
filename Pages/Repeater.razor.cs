using Microsoft.JSInterop;
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

        private List<HistoricoItem> historico = new();

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
                    await SalvarNoHistorico();
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
                    await SalvarNoHistorico();
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

        private class HistoricoItem
        {
            public string Frase { get; set; } = "";
            public int Vezes { get; set; }
            public string Separador { get; set; } = "\n";
            public bool Numerar { get; set; }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CarregarHistorico();
                StateHasChanged();
            }
        }

        private async Task SalvarNoHistorico()
        {
            try
            {
                var novo = new HistoricoItem
                {
                    Frase = frase,
                    Vezes = vezes,
                    Separador = separador,
                    Numerar = numerar
                };
                historico.Insert(0, novo);
                if (historico.Count > 5) historico.RemoveAt(5);
                var json = System.Text.Json.JsonSerializer.Serialize(historico);
                await JS.InvokeVoidAsync("localStorage.setItem", "repeaterHistorico", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar histórico: {ex.Message}");
            }
        }

        private async Task CarregarHistorico()
        {
            try
            {
                var json = await JS.InvokeAsync<string>("localStorage.getItem", "repeaterHistorico");
                if (!string.IsNullOrEmpty(json))
                {
                    historico = System.Text.Json.JsonSerializer.Deserialize<List<HistoricoItem>>(json) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar histórico: {ex.Message}");
            }
        }

        private void CarregarDoHistorico(HistoricoItem item)
        {
            frase = item.Frase;
            vezes = item.Vezes;
            separador = item.Separador;
            numerar = item.Numerar;
            StateHasChanged();
        }
        public void Dispose() => cts?.Cancel();
    }
}
