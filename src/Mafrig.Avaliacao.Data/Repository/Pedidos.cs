using Dapper;
using Mafrig.Avaliacao.Core;
using Mafrig.Avaliacao.Core.DTO;
using Mafrig.Avaliacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Data.Repository
{
    public class Pedidos : IPedidos
    {
        private IConnectionFactory _connectionFactory;
        public Pedidos(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Incluir(Pedido pedido)
        {
            using var conn = _connectionFactory.GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var sqlInserPedido = "INSERT INTO Pedidos (PecuaristaId, DataEntrega) VALUES (@Id, @DataEntrega); SELECT @@IDENTITY;";
                pedido.Id = await conn.ExecuteScalarAsync<int>(sqlInserPedido, new { pedido.Pecuarista.Id, pedido.DataEntrega }, tx);

                var sqlInserItens = "INSERT INTO PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (@PedidoId, @AnimalId, @Quantidade)";
                var itens = pedido.Items.Select(x => new { PedidoId = pedido.Id, AnimalId = x.Animal.Id, x.Quantidade });
                await conn.ExecuteAsync(sqlInserItens, itens, tx);

                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<PedidoItemDTO>> ObterItens(int id)
        {
            var sql =
                "SELECT " +
                "   ped.PedidoId, " +
                "   ped.PedidoItemId, " +
                "   ped.Quantidade, " +
                "   ani.AnimalId, " +
                "   ani.Preco, " +
                "   ani.Descricao " +
                "FROM PedidoItens ped " +
                "   INNER JOIN Animais ani ON ani.AnimalId = ped.AnimalId " +
                "WHERE ped.PedidoId = @id";

            using var conn = _connectionFactory.GetConnection();
            var itens = await conn.QueryAsync<PedidoItemDTO>(sql, new { id });

            return itens;
        }

        public async Task<PedidoCompletoDTO> ObterPorId(int id)
        {
            var sql =
                "SELECT " +
                "   ped.PedidoId, " +
                "   ped.DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome Pecuarista, " +
                "   Sum(ite.Quantidade * ani.Preco) Total " +
                "FROM Pedidos ped " +
                "   INNER JOIN Pecuaristas pec ON ped.PecuaristaId = pec.PecuaristaId " +
                "   INNER JOIN PedidoItens ite ON ite.PedidoId = ped.PedidoId " +
                "   INNER JOIN Animais ani ON ani.AnimalId = ite.AnimalId " +
                "WHERE " +
                "   ped.PedidoId = @id " +
                "GROUP BY " +
                "   ped.PedidoId,  " +
                "   DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome";

            using var conn = _connectionFactory.GetConnection();
            var pedido = await conn.QuerySingleOrDefaultAsync<PedidoCompletoDTO>(sql, new { id });

            return pedido;
        }

        public async Task<IEnumerable<PedidoDTO>> ObterTodos()
        {
            var sql =
                "SELECT " +
                "   ped.PedidoId, " +
                "   ped.DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome Pecuarista, " +
                "   Sum(ite.Quantidade * ani.Preco) Total " +
                "FROM Pedidos ped " +
                "   INNER JOIN Pecuaristas pec ON ped.PecuaristaId = pec.PecuaristaId " +
                "   INNER JOIN PedidoItens ite ON ite.PedidoId = ped.PedidoId " +
                "   INNER JOIN Animais ani ON ani.AnimalId = ite.AnimalId " +
                "GROUP BY " +
                "   ped.PedidoId,  " +
                "   DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome";

            using var conn = _connectionFactory.GetConnection();
            var pedido = await conn.QueryAsync<PedidoDTO>(sql);

            return pedido;
        }

        public async Task<IEnumerable<PedidoDTO>> Pesquisar(int? pedidoId, int? pecuaristaId, DateTime? dataEntregaDe, DateTime? dataEntregaAte)
        {
            var sql =
                "SELECT " +
                "   ped.PedidoId, " +
                "   ped.DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome Pecuarista, " +
                "   Sum(ite.Quantidade * ani.Preco) Total " +
                "FROM Pedidos ped " +
                "   INNER JOIN Pecuaristas pec ON ped.PecuaristaId = pec.PecuaristaId " +
                "   INNER JOIN PedidoItens ite ON ite.PedidoId = ped.PedidoId " +
                "   INNER JOIN Animais ani ON ani.AnimalId = ite.AnimalId " +
                "WHERE  " +
                "   (ped.PedidoId = @pedidoId OR ISNULL(@pedidoId, '') = '') AND " +
                "   (ped.PecuaristaId = @pecuaristaId OR ISNULL(@pecuaristaId, '') = '') AND " +
                "   (ped.DataEntrega >= @dataEntregaDe OR ISNULL(@dataEntregaDe, '') = '') AND " +
                "   (ped.DataEntrega <= @dataEntregaAte OR ISNULL(@dataEntregaAte, '') = '')" +
                "GROUP BY " +
                "   ped.PedidoId,  " +
                "   DataEntrega, " +
                "   pec.PecuaristaId, " +
                "   pec.Nome";

            using var conn = _connectionFactory.GetConnection();
            var pedido = await conn.QueryAsync<PedidoDTO>(sql, new { pedidoId, pecuaristaId, dataEntregaDe, dataEntregaAte });

            return pedido;
        }
    }
}
