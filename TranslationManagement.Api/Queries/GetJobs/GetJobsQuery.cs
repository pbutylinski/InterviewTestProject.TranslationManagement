using MediatR;

namespace TranslationManagement.Api.Queries
{
    public class GetJobsQuery : IRequest<GetJobsQueryResult[]>
    {
    }
}
