using AutoMapper;
using SchoolApi.Models;
using SchoolApi.Dto;

namespace SchoolApi.Profiles;

public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<Enrollment, EnrollmentDTO>();
        CreateMap<EnrollmentDTO, Enrollment>();
    }
}