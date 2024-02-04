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
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserDetails, opt => opt.UseDestinationValue())
                .ForPath(dest => dest.UserDetails.UserType, opt => opt.MapFrom(src => src.UserType))
                .ReverseMap();

            CreateMap<UserExtendedDetails, UserExtendedDetailsDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Complaint, ComplaintDto>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Demand, DemandDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
