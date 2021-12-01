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
    public class AnimaisController : ControllerBase
    {
        private IAnimais _animais;
        private readonly IMapper _mapper;
        public AnimaisController(
            IMapper mapper,
            IAnimais animais)
        {
            _animais = animais;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EntidadeDTO>> Get()
        {
            var animais = await _animais.ObterTodos();
            var dto = _mapper.Map<IEnumerable<AnimalDTO>>(animais);

            return dto;
        }
    }
}
