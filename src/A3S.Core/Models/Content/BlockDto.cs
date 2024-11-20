using A3S.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class BlockDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Block, BlockDto>();
            }
        }
    }
}
