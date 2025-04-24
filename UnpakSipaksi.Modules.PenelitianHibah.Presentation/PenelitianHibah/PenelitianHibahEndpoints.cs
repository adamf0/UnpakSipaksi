using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Modules.PenelitianHibah.Presentation.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Presentation.MemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    public static class PenelitianHibahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
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

            CreateMemberMahasiswa.MapEndpoint(app);
            UpdateMemberMahasiswa.MapEndpoint(app);
            DeleteMemberMahasiswa.MapEndpoint(app);
            UpdateMbkm.MapEndpoint(app);

            DeletePenelitianHibah.MapEndpoint(app);
            GetPenelitianHibah.MapEndpoint(app);
            GetAllPenelitianHibah.MapEndpoint(app);
        }
    }
}
