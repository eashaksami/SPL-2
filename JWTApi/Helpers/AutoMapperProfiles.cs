using AutoMapper;
using JWTApi.Dtos;
using JWTApi.Models;

namespace JWTApi.Helpers
{
    public class AutoMapperprofiles : Profile
    {
        public AutoMapperprofiles()
        {
            CreateMap<Student, UserForLoginDto>();
            CreateMap<Chapter, ChapterDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Question, GetQuestionDto>();
            CreateMap<Question, TestQuestionDto>();
            CreateMap<Image, ImagesDto>();
            CreateMap<Question, AnswerDto>();
            CreateMap<Question, UploadQuestionDto>();
        }
    }
}