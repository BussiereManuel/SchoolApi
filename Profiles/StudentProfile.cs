using AutoMapper;
using SchoolApi.Models;
using SchoolApi.Dto;

namespace SchoolApi.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDTO>();
        CreateMap<StudentDTO, Student>();
    }
}