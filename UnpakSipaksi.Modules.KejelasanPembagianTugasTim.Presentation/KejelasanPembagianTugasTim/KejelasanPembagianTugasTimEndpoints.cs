using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    public static class KejelasanPembagianTugasTimEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKejelasanPembagianTugasTim.MapEndpoint(app);
            UpdateKejelasanPembagianTugasTim.MapEndpoint(app);
            DeleteKejelasanPembagianTugasTim.MapEndpoint(app);
            GetKejelasanPembagianTugasTim.MapEndpoint(app);
            GetAllKejelasanPembagianTugasTim.MapEndpoint(app);
        }
    }
}
