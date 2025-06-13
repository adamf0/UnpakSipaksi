using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiInternal
{
    internal sealed class UpdateAdministrasiInternalCommandHandler(
    IAdministrasiInternalRepository akurasiPenelitianRepository,
    IPenelitianHibahApi penelitianInternalApi,
    IUnitOfWorkAdministrasiInternal unitOfWork)
    : ICommandHandler<UpdateAdministrasiInternalCommand>
    {
        public async Task<Result> Handle(UpdateAdministrasiInternalCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? existPenelitianInternal = await penelitianInternalApi.GetAsync(Guid.Parse(request.uuidPenelitianInternal));
            Domain.AdministrasiInternal.AdministrasiInternal? existingAdministrasiInternal = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAdministrasiInternal is null)
            {
                return Result.Failure(AdministrasiInternalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.AdministrasiInternal.AdministrasiInternal> result = Domain.AdministrasiInternal.AdministrasiInternal.Update(
                Guid.Parse(request.Uuid),
                existingAdministrasiInternal,
                int.Parse(existPenelitianInternal?.Id ?? "0"),
                request.judul,
                request.halamanSampul,
                request.halamanPengesahan,
                request.identitasPengusul,
                request.identitasMahasiswa,
                request.mitraKerjasama,
                request.luaranTargetCapaian,
                request.rab,
                request.latarBelakangRumusanMasalah,
                request.pendekatanPemecahanMasalah,
                request.sota,
                request.penjelasanCapaianRisetKepakaran,
                request.petaJalan,
                request.tahapanPenelitian,
                request.indikatorCapaianPadaRab,
                request.jadwal,
                request.daftarPustaka,
                request.biodataKetuaAnggota,
                request.paktaIntegritas,
                request.suratKetersediaanMitra,
                request.keputusan,
                request.komentar
            );

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
