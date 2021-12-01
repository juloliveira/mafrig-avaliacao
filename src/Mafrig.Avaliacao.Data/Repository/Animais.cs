using Dapper;
using Mafrig.Avaliacao.Core;
using Mafrig.Avaliacao.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Data.Repository
{
    public class Animais : IAnimais
    {
        private IConnectionFactory _connectionFactory;
        public Animais(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Animal> ObterPorId(int id)
        {
            using var conn = _connectionFactory.GetConnection();
            var animal = await conn.QuerySingleOrDefaultAsync<Animal>("SELECT AnimalId Id, Descricao, Preco FROM Animais WHERE AnimalId = @id", new { id });

            return animal;
        }

        public async Task<IEnumerable<Animal>> ObterTodos()
        {
            using var conn = _connectionFactory.GetConnection();
            var animais = await conn.QueryAsync<Animal>("SELECT AnimalId Id, Descricao, Preco FROM Animais");

            return animais;
        }
    }
}
