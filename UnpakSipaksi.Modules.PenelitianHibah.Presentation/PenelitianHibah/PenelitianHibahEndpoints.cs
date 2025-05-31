using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Modules.PenelitianHibah.Presentation.AllMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Presentation.MemberDosen;

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
            DeletePenelitianHibah.MapEndpoint(app);

            GetAllMemberDosen.MapEndpoint(app);
            GetMemberDosen.MapEndpoint(app);
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

            //[PR] single data
            CreateSubstansiUsulan.MapEndpoint(app);
            UpdateSubstansiUsulan.MapEndpoint(app);
            DeleteSubstansiUsulan.MapEndpoint(app);

            //[PR] list
            CreateLuaran.MapEndpoint(app);
            UpdateLuaran.MapEndpoint(app);
            DeleteLuaran.MapEndpoint(app);

            //[PR] list
            CreateRAB.MapEndpoint(app);
            UpdateRAB.MapEndpoint(app);
            DeleteRAB.MapEndpoint(app);

            //[PR] list
            CreateDokumenPendukung.MapEndpoint(app);
            UpdateDokumenPendukung.MapEndpoint(app);
            DeleteDokumenPendukung.MapEndpoint(app);

            //[PR] list
            CreateDokumenKontrak.MapEndpoint(app);
            UpdateDokumenKontrak.MapEndpoint(app);
            DeleteDokumenKontrak.MapEndpoint(app);
        }
    }
}
