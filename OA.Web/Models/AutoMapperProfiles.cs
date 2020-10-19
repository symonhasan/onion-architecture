using AutoMapper;
using OA.Data;

namespace OA.Web.Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TouristPlace, TouristPlaceViewModel>();
            CreateMap<TouristPlaceViewModel, TouristPlace>();
        }
    }
}
