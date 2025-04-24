using MediatR;
using UnpakSipaksi.Common.Domain;

using IPenelitianHibahApi           = UnpakSipaksi.Modules.PenelitianHibah.PublicApi.IPenelitianHibahApi;
using PenelitianHibahResponseApi    = UnpakSipaksi.Modules.PenelitianHibah.PublicApi.PenelitianHibahResponse;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

using SkemaDefaultResponse      = UnpakSipaksi.Modules.PenelitianHibah.PublicApi.SkemaDefaultResponse;
using RisetDefaultResponse      = UnpakSipaksi.Modules.PenelitianHibah.PublicApi.RisetDefaultResponse;
using RumpunIlmuDefaultResponse = UnpakSipaksi.Modules.PenelitianHibah.PublicApi.RumpunIlmuDefaultResponse;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PublicApi
{
    internal sealed class PenelitianHibahApi(ISender sender) : IPenelitianHibahApi
    {
        public async Task<PenelitianHibahResponseApi?> GetAsync(Guid PenelitianHibahUuid, CancellationToken cancellationToken = default)
        {
            Result<PenelitianHibahDefaultResponse> result = await sender.Send(new GetPenelitianHibahDefaultQuery(PenelitianHibahUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PenelitianHibahResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.NIDN,
                result.Value.Judul,
                result.Value.TahunPengajuan,
                result.Value.Skema == null? null:new SkemaDefaultResponse(
                    result.Value.Skema.IdSkema,
                    result.Value.Skema.UuidSkema,
                    result.Value.Skema.NamaSkema,
                    result.Value.Skema.TKT,
                    result.Value.Skema.IdKategoriTkt,
                    result.Value.Skema.UuidKategoriTkt,
                    result.Value.Skema.NamaKategoriTkt
                ),
                result.Value.Riset==null? null: new RisetDefaultResponse(
                    result.Value.Riset.IdPrioritasRiset,
                    result.Value.Riset.UuidPrioritasRiset,
                    result.Value.Riset.NamaPrioritasRiset,

                    result.Value.Riset.IdFokusPenelitian,
                    result.Value.Riset.UuidFokusPenelitian,
                    result.Value.Riset.NamaFokusPenelitian,

                    result.Value.Riset.IdTemaPenelitian,
                    result.Value.Riset.UuidTemaPenelitian,
                    result.Value.Riset.NamaTemaPenelitian,

                    result.Value.Riset.IdTopikPenelitian,
                    result.Value.Riset.UuidTopikPenelitian,
                    result.Value.Riset.NamaTopikPenelitian
                ),
                result.Value.RumpunIlmu==null? null: new RumpunIlmuDefaultResponse(
                    result.Value.RumpunIlmu.IdRumpunIlmu1,
                    result.Value.RumpunIlmu.UuidRumpunIlmu1,
                    result.Value.RumpunIlmu.NamaRumpunIlmu1,

                    result.Value.RumpunIlmu.IdRumpunIlmu2,
                    result.Value.RumpunIlmu.UuidRumpunIlmu2,
                    result.Value.RumpunIlmu.NamaRumpunIlmu2,

                    result.Value.RumpunIlmu.IdRumpunIlmu3,
                    result.Value.RumpunIlmu.UuidRumpunIlmu3,
                    result.Value.RumpunIlmu.NamaRumpunIlmu3
                ),
                result.Value.LamaKegiatan,
                result.Value.Status,
                result.Value.Type
            );
        }
    }

}
