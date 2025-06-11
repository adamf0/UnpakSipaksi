using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    public static class PenelitianPkmEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            GetAllPenelitianPkm.MapEndpoint(app);
            GetPenelitianPkm.MapEndpoint(app);
            CreatePenelitianPkm.MapEndpoint(app);
            UpdatePenelitianPkm.MapEndpoint(app);
            UpdateRumpunIlmu.MapEndpoint(app);
            DeletePenelitianPkm.MapEndpoint(app);

            GetAllMemberDosen.MapEndpoint(app);
            GetMemberDosen.MapEndpoint(app);
            CreateMemberDosen.MapEndpoint(app);
            UpdateMemberDosen.MapEndpoint(app);
            DeleteMemberDosen.MapEndpoint(app);
            ApprovalMemberDosen.MapEndpoint(app);

            GetAllMemberNonDosen.MapEndpoint(app);
            GetMemberNonDosen.MapEndpoint(app);
            CreateMemberNonDosen.MapEndpoint(app);
            UpdateMemberNonDosen.MapEndpoint(app);
            DeleteMemberNonDosen.MapEndpoint(app);

            GetAllMemberMahasiswa.MapEndpoint(app);
            GetMemberMahasiswa.MapEndpoint(app);
            CreateMemberMahasiswa.MapEndpoint(app);
            UpdateMemberMahasiswa.MapEndpoint(app);
            DeleteMemberMahasiswa.MapEndpoint(app);
            UpdateMbkm.MapEndpoint(app);

            GetSubstansiUsulan.MapEndpoint(app);
            CreateSubstansiUsulan.MapEndpoint(app);
            UpdateSubstansiUsulan.MapEndpoint(app);
            DeleteSubstansiUsulan.MapEndpoint(app);

            GetAllLuaran.MapEndpoint(app);
            GetLuaran.MapEndpoint(app);
            CreateLuaran.MapEndpoint(app);
            UpdateLuaran.MapEndpoint(app);
            DeleteLuaran.MapEndpoint(app);

            GetAllRab.MapEndpoint(app);
            GetRab.MapEndpoint(app);
            CreateRAB.MapEndpoint(app);
            UpdateRAB.MapEndpoint(app);
            DeleteRAB.MapEndpoint(app);

        }
    }
}
