using AutoMapper;
using Skyfri.Models;
using Skyfri.ViewModels;

namespace Skyfri.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Tenant, TenantViewModel>();
            CreateMap<TenantUpdateModel, Tenant>();

            CreateMap<Portfolio, PortfolioViewModel>();
            CreateMap<PortfolioUpdateModel, Portfolio>();

            CreateMap<Plant, PlantViewModel>();
            CreateMap<PlantUpdateModel, Plant>();
        }
    }
}
