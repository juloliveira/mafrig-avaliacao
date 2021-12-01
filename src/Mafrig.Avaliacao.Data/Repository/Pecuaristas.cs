using Mafrig.Avaliacao.Core;
using Mafrig.Avaliacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Data.Repository
{
    public class Pecuaristas : IPecuaristas
    {
        private IConnectionFactory _connectionFactory;
        public Pecuaristas(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Pecuarista>> ObterTodos()
        {
            using var conn = _connectionFactory.GetConnection();
            var pecuaristas = await conn.QueryAsync<Pecuarista>("SELECT PecuaristaId Id, Nome FROM Pecuaristas");

            return pecuaristas;
        }

        public async Task<Pecuarista> ObterPorId(int id)
        {
            using var conn = _connectionFactory.GetConnection();
            var pecuarista = await conn.QuerySingleOrDefaultAsync<Pecuarista>("SELECT PecuaristaId Id, Nome FROM Pecuaristas WHERE PecuaristaId = @id", new { id });

            return pecuarista;
        }
    }
}
