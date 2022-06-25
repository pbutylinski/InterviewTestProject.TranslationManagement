using MediatR;

namespace TranslationManagement.Api.Queries
{
    public class GetTranslatorsQuery : IRequest<GetTranslatorsQueryResult[]>
    {
    }
}
