using AutoMapper;
using Mafrig.Avaliacao.Core.DTO;
using Mafrig.Avaliacao.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Api.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Pecuarista, EntidadeDTO>();

            CreateMap<Animal, AnimalDTO>()
                .ForMember(x => x.Nome, o => o.MapFrom(src => src.Descricao));
        }
    }
}
