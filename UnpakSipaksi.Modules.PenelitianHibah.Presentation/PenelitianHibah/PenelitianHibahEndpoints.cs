using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    public static class PenelitianHibahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            GetAllPenelitianHibah.MapEndpoint(app);
            GetPenelitianHibah.MapEndpoint(app);
            CreatePenelitianHibah.MapEndpoint(app);
            UpdatePenelitianHibah.MapEndpoint(app);
            UpdateSkema.MapEndpoint(app);
            UpdateRumpunIlmu.MapEndpoint(app);
            UpdateLamaKegiatan.MapEndpoint(app);
            UpdateStatus.MapEndpoint(app);
            DeletePenelitianHibah.MapEndpoint(app);

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

            GetAllDokumenPendukung.MapEndpoint(app);
            GetDokumenPendukung.MapEndpoint(app);
            CreateDokumenPendukung.MapEndpoint(app);
            UpdateDokumenPendukung.MapEndpoint(app);
            DeleteDokumenPendukung.MapEndpoint(app);

            GetAllDokumenKontrak.MapEndpoint(app);
            GetDokumenKontrak.MapEndpoint(app);
            CreateDokumenKontrak.MapEndpoint(app);
            UpdateDokumenKontrak.MapEndpoint(app);
            DeleteDokumenKontrak.MapEndpoint(app);
        }
    }
}
