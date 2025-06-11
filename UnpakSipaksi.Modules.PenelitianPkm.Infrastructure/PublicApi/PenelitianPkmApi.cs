using MediatR;
using UnpakSipaksi.Common.Domain;

using IPenelitianPkmApi = UnpakSipaksi.Modules.PenelitianPkm.PublicApi.IPenelitianPkmApi;
using PenelitianPkmResponseApi = UnpakSipaksi.Modules.PenelitianPkm.PublicApi.PenelitianPkmResponse;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;

using RumpunIlmuDefaultResponse = UnpakSipaksi.Modules.PenelitianPkm.PublicApi.RumpunIlmuDefaultResponse;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PublicApi
{
    internal sealed class PenelitianPkmApi(ISender sender) : IPenelitianPkmApi
    {
        public async Task<PenelitianPkmResponseApi?> GetAsync(Guid PenelitianPkmUuid, CancellationToken cancellationToken = default)
        {
            Result<PenelitianPkmDefaultResponse> result = await sender.Send(new GetPenelitianPkmDefaultQuery(PenelitianPkmUuid.ToString()), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PenelitianPkmResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.NIDN,
                result.Value.Judul,
                result.Value.TahunPengajuan,
                string.IsNullOrEmpty(result.Value.KategoriProgramPengabdian?.Id) ? null : new KategoriProgramPengabdianDefaultResponse(
                    result.Value.KategoriProgramPengabdian?.Id,
                    result.Value.KategoriProgramPengabdian?.Uuid,
                    result.Value.KategoriProgramPengabdian?.Nama
                ),
                string.IsNullOrEmpty(result.Value.ProgramPengabdian?.FokusPengabdianId) && string.IsNullOrEmpty(result.Value.ProgramPengabdian?.RirnId) ? null : new ProgramPengabdianDefaultResponse(
                    result.Value.ProgramPengabdian?.FokusPengabdianId,
                    result.Value.ProgramPengabdian?.UuidFokusPengabdian,
                    result.Value.ProgramPengabdian?.NamaFokusPengabdian,

                    result.Value.ProgramPengabdian?.RirnId,
                    result.Value.ProgramPengabdian?.UuidRirn,
                    result.Value.ProgramPengabdian?.NamaRirn
                ),
                result.Value.RumpunIlmu == null ? null : new RumpunIlmuDefaultResponse(
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
                result.Value.Status,
                result.Value.Type
            );
        }
    }

}
