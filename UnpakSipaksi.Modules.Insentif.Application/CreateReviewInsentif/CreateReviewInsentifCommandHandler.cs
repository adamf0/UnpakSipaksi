using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Application.CreateReviewInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm;
using UnpakSipaksi.Modules.JenisPublikasi.PublicApi;

internal sealed class CreateReviewInsentifCommandHandler(
    IVerifikasiFakultasRepository verifikasiFakultasRepository,
    IVerifikasiLppmRepository verifikasiLppmRepository,
    IJenisPublikasiApi jenisPublikasiApi,
    IUnitOfWorkVerifikasiFakultas UnitOfWorkVerifikasiFakultas,
    IUnitOfWorkVerifikasiLppm UnitOfWorkVerifikasiLppm)
    : ICommandHandler<CreateReviewInsentifCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReviewInsentifCommand request, CancellationToken cancellationToken)
    {
        var uuid = Guid.Parse(request.Uuid);
        var jenisPublikasi = await jenisPublikasiApi.GetAsync(Guid.Parse(request.StatusJurnal));
        var jenisPublikasiId = int.Parse(jenisPublikasi?.Id ?? "0");

        if (request.Type == "fakultas")
        {
            var prev = await verifikasiFakultasRepository.GetAsync(uuid);

            var result = VerifikasiFakultas.Review(
                uuid,
                prev,
                request.BuktiPublikasi.ToEnumFromString<BuktiPublikasi>(),
                jenisPublikasiId,
                request.PeranPenulis.ToEnumFromString<Peran>(),
                request.JumlahPenulis,
                request.JenisJurnal.ToEnumFromString<JenisJurnal>(),
                request.LibatkanMahasiswa.ToEnumFromString<LibatkanMahasiswa>(),
                jenisPublikasi?.Sbu ?? 0,
                request.StatusPengajuan.ToEnumFromString<StatusPengajuan>(),
                request.Catatan
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            await UnitOfWorkVerifikasiFakultas.SaveChangesAsync(cancellationToken);
            return Result.Success(result.Value.Uuid);
        }
        else
        {
            var prev = await verifikasiLppmRepository.GetAsync(uuid);

            var result = VerifikasiLppm.Review(
                uuid,
                prev,
                request.BuktiPublikasi.ToEnumFromString<BuktiPublikasi>(),
                jenisPublikasiId,
                request.PeranPenulis.ToEnumFromString<Peran>(),
                request.JumlahPenulis,
                request.JenisJurnal.ToEnumFromString<JenisJurnal>(),
                request.LibatkanMahasiswa.ToEnumFromString<LibatkanMahasiswa>(),
                jenisPublikasi?.Sbu ?? 0,
                request.StatusPengajuan.ToEnumFromString<StatusPengajuan>(),
                request.Catatan
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            await UnitOfWorkVerifikasiLppm.SaveChangesAsync(cancellationToken);
            return Result.Success(result.Value.Uuid);
        }
    }
}
