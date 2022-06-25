using AutoMapper;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Commands.CreateJobCommand, TranslationJob>();
            CreateMap<TranslationJob, DataAccess.Models.TranslationJob>();
            CreateMap<TranslationJob, Queries.GetJobsQueryResult>();
            CreateMap<DataAccess.Models.TranslationJob, TranslationJob>();
            CreateMap<DataAccess.Models.TranslationJob, Queries.GetJobsQueryResult>();

            CreateMap<Commands.CreateTranslatorCommand, Translator>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => string.Empty));
            CreateMap<Translator, DataAccess.Models.Translator>();
            CreateMap<Translator, Queries.GetTranslatorsQueryResult>();
            CreateMap<DataAccess.Models.Translator, Translator>();
            CreateMap<DataAccess.Models.Translator, Queries.GetTranslatorsQueryResult>();
        }
    }
}
