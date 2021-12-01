using Mafrig.Avaliacao.Core.Exceptions;

namespace Mafrig.Avaliacao.Core
{
    public class Pecuarista : Entidade
    {
        protected Pecuarista()
        {

        }
        public Pecuarista(string nome)
        {
            if (string.IsNullOrEmpty(nome)) throw new ModeloInvalidoException(this);

            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
