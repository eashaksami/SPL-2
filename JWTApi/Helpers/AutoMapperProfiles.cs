using AutoMapper;
using EBET.Dtos;
using EBET.Models;

namespace EBET.Helpers
{
    public class AutoMapperprofiles : Profile
    {
        public AutoMapperprofiles()
        {
            CreateMap<User, UserForLoginDto>();
            CreateMap<Chapter, ChapterDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Question, GetQuestionDto>();
            CreateMap<Question, TestQuestionDto>();
            CreateMap<Image, ImagesDto>();
            CreateMap<Question, AnswerDto>();
            CreateMap<Question, UploadQuestionDto>();
            CreateMap<TestExam, ProgressInfoDto>();
        }
    }
}
