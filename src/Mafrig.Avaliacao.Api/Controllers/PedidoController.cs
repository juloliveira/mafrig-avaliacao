using AutoMapper;
using Mafrig.Avaliacao.Api.Model;
using Mafrig.Avaliacao.Core;
using Mafrig.Avaliacao.Core.DTO;
using Mafrig.Avaliacao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mafrig.Avaliacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IAnimais _animais;
        private readonly IPedidos _pedidos;
        private readonly IPecuaristas _pecuaristas;
        private readonly IMapper _mapper;
        public PedidoController(
            IMapper mapper,
            IAnimais animais,
            IPecuaristas pecuaristas,
            IPedidos pedidos)
        {
            _animais = animais;
            _pecuaristas = pecuaristas;
            _pedidos = pedidos;
            _mapper = mapper;
        }

        [HttpGet("pesquisar")]
        public async Task<IEnumerable<PedidoDTO>> Pesquisar([FromQuery] PesquisarRequest get)
        {
            var pedidos = await _pedidos.Pesquisar(get.PedidoId, get.PecuaristaId, get.DataEntregaDe, get.DataEntregaAte);

            return pedidos;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoDTO>> Get()
        {
            var pedidos = await _pedidos.ObterTodos();

            return pedidos;
        }

        
        [HttpGet("{id}")]
        public async Task<PedidoCompletoDTO> Get(int id)
        {
            var pedido = await _pedidos.ObterPorId(id);
            pedido.Itens = await _pedidos.ObterItens(pedido.PedidoId);

            return pedido;
        }

        
        [HttpPost]
        public async Task<Resultado> Post([FromBody] PedidoRequest post)
        {
            var pecuarista = await _pecuaristas.ObterPorId(post.PecuaristaId);
            var pedido = new Pedido(pecuarista, post.DataEntrega);

            foreach (var pedidoItem in post.Itens)
            {
                var animal = await _animais.ObterPorId(pedidoItem.AnimalId);
                pedido.AdicionarItem(animal, pedidoItem.Quantidade);
            }

            await _pedidos.Incluir(pedido);

            return Resultado.Ok;
        }

        
        [HttpPut("{id}")]
        public async Task<Resultado> Put(int id, [FromBody] PedidoRequest put)
        {
            var pedido = await _pedidos.ObterPorId(id);

            return null;
        }

    }
}
