using AutoMapper;
using Common.DTOs;
using DomainLayer.Models.Complaints;
using DomainLayer.Models.Demands;
using DomainLayer.Models.Users;

namespace DomainLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserExtendedDetails, UserExtendedDetailsDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;

            CreateMap<Demand, DemandDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Complaint, ComplaintDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
