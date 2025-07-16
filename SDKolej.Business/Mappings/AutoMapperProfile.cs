using AutoMapper;
using SDKolej.Business.DTOs;
using SDKolej.Data.Entities;

namespace SDKolej.Business.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Parent, ParentDto>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();
            CreateMap<Absence, AbsenceDto>().ReverseMap();
            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<FileUpload, FileUploadDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<ClassCourse, ClassCourseDto>().ReverseMap();
            CreateMap<TeacherCourse, TeacherCourseDto>().ReverseMap();
            CreateMap<StudentParent, StudentParentDto>().ReverseMap();
        }
    }
} 