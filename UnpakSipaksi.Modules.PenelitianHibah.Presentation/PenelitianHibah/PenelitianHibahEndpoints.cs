using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    public static class PenelitianHibahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            //[PR] single data
            CreatePenelitianHibah.MapEndpoint(app);
            UpdatePenelitianHibah.MapEndpoint(app);
            UpdateSkema.MapEndpoint(app);
            UpdateRumpunIlmu.MapEndpoint(app);
            UpdateLamaKegiatan.MapEndpoint(app);

            //[PR] list
            CreateMemberDosen.MapEndpoint(app);
            UpdateMemberDosen.MapEndpoint(app);
            DeleteMemberDosen.MapEndpoint(app);
            ApprovalMemberDosen.MapEndpoint(app);

            //[PR] list
            CreateMemberNonDosen.MapEndpoint(app);
            UpdateMemberNonDosen.MapEndpoint(app);
            DeleteMemberNonDosen.MapEndpoint(app);

            //[PR] list
            CreateMemberMahasiswa.MapEndpoint(app);
            UpdateMemberMahasiswa.MapEndpoint(app);
            DeleteMemberMahasiswa.MapEndpoint(app);
            UpdateMbkm.MapEndpoint(app);

            //[PR] list
            DeletePenelitianHibah.MapEndpoint(app);
            GetPenelitianHibah.MapEndpoint(app);
            GetAllPenelitianHibah.MapEndpoint(app);

            //[PR] single data
            CreateSubstansiUsulan.MapEndpoint(app);
            UpdateSubstansiUsulan.MapEndpoint(app);
            DeleteSubstansiUsulan.MapEndpoint(app);

            //[PR] list
            CreateLuaran.MapEndpoint(app);
            UpdateLuaran.MapEndpoint(app);
            DeleteLuaran.MapEndpoint(app);
        }
    }
}
