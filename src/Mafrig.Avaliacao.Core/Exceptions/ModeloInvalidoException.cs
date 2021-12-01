using System;
using System.Collections.Generic;
using System.Text;

namespace Mafrig.Avaliacao.Core.Exceptions
{
    public class ModeloInvalidoException : Exception
    {
        public ModeloInvalidoException(Entidade entidade) : base($"Modelo {nameof(entidade)} inválido!")
        {
        }
    }
}
