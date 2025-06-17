using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.JenisPublikasi.PublicApi;

namespace UnpakSipaksi.Modules.Insentif.Application.UpdateInsentifJurnal
{
    internal sealed class UpdateInsentifJurnalCommandHandler(
    IInsentifRepository InsentifPenelitianRepository,
    IJenisPublikasiApi JenisPublikasiApi,
    IUnitOfWorkInsentif unitOfWork)
    : ICommandHandler<UpdateInsentifJurnalCommand>
    {
        public async Task<Result> Handle(UpdateInsentifJurnalCommand request, CancellationToken cancellationToken)
        {
            Domain.Insentif.Insentif? existingInsentif = await InsentifPenelitianRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingInsentif is null)
            {
                return Result.Failure(InsentifErrors.NotFound(request.Uuid));
            }

            JenisPublikasiResponse? JenisPublikasi = await JenisPublikasiApi.GetAsync(Guid.Parse(request.IndexJenisPublikasi));
            if (JenisPublikasi is null)
            {
                return Result.Failure<Guid>(InsentifErrors.NotFoundIndexJenisPublikasi(request.IndexJenisPublikasi));

            }

            Result<Domain.Insentif.Insentif> asset = Domain.Insentif.Insentif.UpdateInsentifJurnal(
                existingInsentif!,
                request.Nidn,
                request.JudulArtikel,
                request.NamaJurnalPenerbit,
                request.JumlahPenulis,
                int.Parse(JenisPublikasi.Id),
                request.Link,
                request.JenisJurnal.ToEnumFromInt<JenisJurnal>(), //1: internal 0:external
                request.Peran.ToEnumFromString<Peran>(), //First Author, Co Author
                request.TanggalTerbit,
                request.Volume,
                request.Edisi,
                request.Halaman,
                request.DOI,
                request.LibatkanMahasiswa.ToEnumFromInt<LibatkanMahasiswa>() //1: ya 0: tidak
            );

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
