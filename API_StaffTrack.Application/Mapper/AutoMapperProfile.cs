using AutoMapper;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Models.Common;
using API_StaffTrack.Models.Response;

namespace API_StaffTrack.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Common


            //Main
            CreateMap<Employee, MRes_Employee>();

        }
    }
}
