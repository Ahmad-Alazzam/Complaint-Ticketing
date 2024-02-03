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

            CreateMap<UserExtendedDetails, UserExtendedDetailsDto>().ReverseMap();

            CreateMap<Demand, DemandDto>().ReverseMap();

            CreateMap<Complaint, ComplaintDto>().ReverseMap();
        }
    }
}
