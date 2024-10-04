using AutoMapper;
using SchoolApi.Models;
using SchoolApi.Dto;

namespace SchoolApi.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDTO>();
        CreateMap<CourseDTO, Course>();
    }
}