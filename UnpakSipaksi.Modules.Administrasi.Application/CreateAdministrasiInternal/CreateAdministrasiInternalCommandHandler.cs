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
namespace UnpakSipaksi.Modules.Administrasi.Application.CreateAdministrasiInternal
{
    internal sealed class CreateAdministrasiInternalCommandHandler(
    IAdministrasiInternalRepository akurasiPenelitianRepository,
    IPenelitianHibahApi penelitianInternalApi,
    IUnitOfWorkAdministrasiInternal unitOfWork)
    : ICommandHandler<CreateAdministrasiInternalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateAdministrasiInternalCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? existPenelitianInternal = await penelitianInternalApi.GetAsync(Guid.Parse(request.uuidPenelitianInternal));

            Result<Domain.AdministrasiInternal.AdministrasiInternal> result = Domain.AdministrasiInternal.AdministrasiInternal.Create(
                int.Parse(existPenelitianInternal?.Id ?? "0"),
                request.level,
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
                return Result.Failure<Guid>(result.Error);
            }

            akurasiPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
