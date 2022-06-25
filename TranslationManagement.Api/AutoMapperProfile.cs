using AutoMapper;

namespace TranslationManagement.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Commands.CreateJobCommand, Domain.TranslationJob>();
            CreateMap<Domain.TranslationJob, DataAccess.Models.TranslationJob>();
            CreateMap<DataAccess.Models.TranslationJob, Queries.GetJobsQueryResult>();
        }
    }
}
