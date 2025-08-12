using AutoMapper;
using AZFlow360.Application.Features.Auth.Commands;
using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AZFlow360.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth
            CreateMap<RegisterCommand, User>();

            // Products
            CreateMap<CreateProductCommand, Product>();
        }
    }
}
