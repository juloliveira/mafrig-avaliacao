namespace Mafrig.Avaliacao.Api.Model
{
    public class Resultado
    {
        public Resultado(int codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }
        public int Codigo { get; set; }
        public string Mensagem { get; set; }

        public static Resultado Ok => new Resultado(200, "Ok");
    }
}
