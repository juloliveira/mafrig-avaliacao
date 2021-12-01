using AutoMapper;
using Mafrig.Avaliacao.Core.DTO;
using Mafrig.Avaliacao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecuaristasController : ControllerBase
    {
        private IPecuaristas _pecuaristas;
        private readonly IMapper _mapper;
        public PecuaristasController(
            IMapper mapper,
            IPecuaristas pecuaristas)
        {
            _pecuaristas = pecuaristas;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EntidadeDTO>> Get()
        {
            var pecuaristas = await _pecuaristas.ObterTodos();
            var dto = _mapper.Map<IEnumerable<EntidadeDTO>>(pecuaristas);

            return dto;
        }

        [HttpGet("{id}")]
        public async Task<EntidadeDTO> Get(int id)
        {
            var pecuarista = await _pecuaristas.ObterPorId(id);
            var dto = _mapper.Map<EntidadeDTO>(pecuarista);

            return dto;
        }
    }
}
