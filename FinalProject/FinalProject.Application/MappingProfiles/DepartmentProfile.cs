using AutoMapper;
using FinalProject.Application.DTOs.Department;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, GetDepartmentDto>().ReverseMap();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>().ForMember(c => c.Id, opt => opt.Ignore())
                .ForSourceMember(c => c.Name, opt => opt.DoNotValidate());
            CreateMap<Department, DepartmentItemDto>();
        }
    }
}
