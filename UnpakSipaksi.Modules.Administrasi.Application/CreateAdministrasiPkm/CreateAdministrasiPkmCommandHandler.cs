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

namespace UnpakSipaksi.Modules.Administrasi.Application.CreateAdministrasiPkm
{
    internal sealed class CreateAdministrasiPkmCommandHandler(
    IAdministrasiPkmRepository akurasiPenelitianRepository,
    IPenelitianPkmApi penelitianPkmApi,
    IUnitOfWorkAdministrasiPkm unitOfWork)
    : ICommandHandler<CreateAdministrasiPkmCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateAdministrasiPkmCommand request, CancellationToken cancellationToken)
        {
            PenelitianPkmResponse? existPenelitianPkm = await penelitianPkmApi.GetAsync(Guid.Parse(request.uuidPenelitianPkm));

            Result<Domain.AdministrasiPkm.AdministrasiPkm> result = Domain.AdministrasiPkm.AdministrasiPkm.Create(
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
                return Result.Failure<Guid>(result.Error);
            }

            akurasiPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
