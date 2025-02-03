using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    public static class AuthorSintaEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateAuthorSinta.MapEndpoint(app);
            UpdateAuthorSinta.MapEndpoint(app);
            DeleteAuthorSinta.MapEndpoint(app);
            GetAuthorSinta.MapEndpoint(app);
            GetAllAuthorSinta.MapEndpoint(app);
        }
    }
}
