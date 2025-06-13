using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;

namespace UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiPkm
{
    internal sealed class UpdateAdministrasiPkmCommandHandler(
    IAdministrasiPkmRepository akurasiPenelitianRepository,
    IPenelitianPkmApi penelitianPkmApi,
    IUnitOfWorkAdministrasiPkm unitOfWork)
    : ICommandHandler<UpdateAdministrasiPkmCommand>
    {
        public async Task<Result> Handle(UpdateAdministrasiPkmCommand request, CancellationToken cancellationToken)
        {
            PenelitianPkmResponse? existPenelitianPkm = await penelitianPkmApi.GetAsync(Guid.Parse(request.uuidPenelitianPkm));
            Domain.AdministrasiPkm.AdministrasiPkm? existingAdministrasiPkm = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAdministrasiPkm is null)
            {
                return Result.Failure(AdministrasiPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.AdministrasiPkm.AdministrasiPkm> result = Domain.AdministrasiPkm.AdministrasiPkm.Update(
                Guid.Parse(request.Uuid),
                existingAdministrasiPkm,
                int.Parse(existPenelitianPkm?.Id ?? "0"),
                request.judul,
                request.halamanSampul,
                request.halamanPengesahan,
                request.identitasPengusul,
                request.mitraPkm,
                request.luaranTargetCapaian,
                request.rab,
                request.ringkasanKataKunci,
                request.analisisSituasi,
                request.permasalahanMitra,
                request.solusiPermasalahan,
                request.metodePelaksanaan,
                request.jadwal,
                request.daftarPustaka,
                request.biodataKetuaAnggota,
                request.gambaranIptek,
                request.petaLokasiMitra,
                request.suratPernyataanKetuaPelaksana,
                request.suratKetersediaanMitra,
                request.melibatkanMahasiswa,
                request.mendukungIKU,
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
