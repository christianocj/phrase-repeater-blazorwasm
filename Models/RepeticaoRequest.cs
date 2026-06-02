namespace Repetidor.Models
{
    public class RepeticaoRequest
    {
       
            public string Frase { get; set; } = "";
            public int Vezes { get; set; }
            public string Separador { get; set; } = "\n";
            public bool Numerar { get; set; }
    }
}
