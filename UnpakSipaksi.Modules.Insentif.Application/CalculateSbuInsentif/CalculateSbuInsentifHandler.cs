using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.CalculateSbuInsentif;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.JenisPublikasi.PublicApi;

internal sealed class CalculateSbuInsentifCommandHandler(
    IJenisPublikasiApi jenisPublikasiApi)
    : ICommandHandler<CalculateSbuInsentifCommand, SbuInsentifResponse>
{
    public async Task<Result<SbuInsentifResponse>> Handle(CalculateSbuInsentifCommand request, CancellationToken cancellationToken)
    {
        var jenisPublikasi = await jenisPublikasiApi.GetAsync(Guid.Parse(request.StatusJurnal));
        var jenisPublikasiId = int.Parse(jenisPublikasi?.Id ?? "0");

        var input = new InsentifInput
        {
            JenisJurnal = request.JenisJurnal.ToEnumFromString<JenisJurnal>(),
            Mahasiswa = request.LibatkanMahasiswa.ToEnumFromString<LibatkanMahasiswa>() == LibatkanMahasiswa.Ya ? true : false,
            PeranPenulis = request.PeranPenulis.ToEnumFromString<Peran>(),
            SBU = jenisPublikasi?.Sbu ?? 0,
            JumlahCoAuthor = request.JumlahPenulis
        };
        var calculator = InsentifCalculator.Hitung(input);

        if (calculator.IsFailure)
            return Result.Failure<SbuInsentifResponse>(calculator.Error);

        if (calculator.Value.PorsiSBU < 1)
            return Result.Failure<SbuInsentifResponse>(VerifikasiFakultasErrors.InvalidPorsi());

        if (calculator.Value.TotalInsentif <= 0)
            return Result.Failure<SbuInsentifResponse>(VerifikasiFakultasErrors.InvalidCalculateMoney());

        return Result.Success(new SbuInsentifResponse() {
            SBU=jenisPublikasi?.Sbu ?? 0,
            IFA=calculator.Value.IFA,
            ICA= calculator.Value.ICA,
            TotalInsentif= calculator.Value.TotalInsentif,
            PorsiSBU=calculator.Value.PorsiSBU
        });
    }
}
