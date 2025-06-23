using AutoMapper;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Models.Common;
using API_StaffTrack.Models.Response;
using API_StaffTrack.Models.Request;

namespace API_StaffTrack.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Common


            //Main
            CreateMap<MReq_Employee, Employee>()
    .ForMember(dest => dest.JoinDate,
               opt => opt.MapFrom(src => src.JoinDate.HasValue ? DateOnly.FromDateTime(src.JoinDate.Value) : (DateOnly?)null));

            CreateMap<Employee, MRes_Employee>()
                .ForMember(dest => dest.JoinDate,
                           opt => opt.MapFrom(src => src.JoinDate.HasValue ? src.JoinDate.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null));
            CreateMap<MReq_WorkPlan, WorkPlan>()
            .ForMember(dest => dest.WorkDate,
               opt => opt.MapFrom(src =>  DateOnly.FromDateTime(src.WorkDate) ));

            CreateMap<WorkPlan, MRes_WorkPlan>()
                .ForMember(dest => dest.WorkDate,
                           opt => opt.MapFrom(src =>src.WorkDate.ToDateTime(TimeOnly.MinValue) ));
            CreateMap<Notification, MRes_Notification>();
            CreateMap<MReq_Notification, Notification>();

            CreateMap<MReq_AttendanceRecord, AttendanceRecord>();
            CreateMap<AttendanceRecord, MRes_AttendanceRecord>();

      
     
            CreateMap<MReq_LeaveRequest, LeaveRequest>()
            .ForMember(dest => dest.LeaveDate,
               opt => opt.MapFrom(src => DateOnly.FromDateTime(src.LeaveDate)));

            CreateMap<LeaveRequest, MRes_LeaveRequest>()
                .ForMember(dest => dest.LeaveDate,
                           opt => opt.MapFrom(src => src.LeaveDate.ToDateTime(TimeOnly.MinValue)))
             .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
            .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Employee.Email));

            CreateMap<MReq_MonthlyReport, MonthlyReport>();
            CreateMap<MonthlyReport, MRes_MonthlyReport>();

        }
    }
}
