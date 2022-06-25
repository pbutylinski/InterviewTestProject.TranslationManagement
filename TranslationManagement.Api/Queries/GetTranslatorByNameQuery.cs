using MediatR;

namespace TranslationManagement.Api.Queries
{
    public class GetTranslatorByNameQuery : IRequest<GetTranslatorsQueryResult>
    {
        public string Name { get; set; }
    }
}
