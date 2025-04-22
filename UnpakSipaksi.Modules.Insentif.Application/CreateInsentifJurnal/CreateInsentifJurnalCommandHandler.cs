using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.JenisPublikasi.PublicApi;

namespace UnpakSipaksi.Modules.Insentif.Application.CreateInsentifJurnal
{
    internal sealed class CreateInsentifProsidingCommandHandler(
    IInsentifRepository InsentifPenelitianRepository,
    IJenisPublikasiApi JenisPublikasiApi,
    IUnitOfWorkInsentif unitOfWork)
    : ICommandHandler<CreateInsentifJurnalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateInsentifJurnalCommand request, CancellationToken cancellationToken)
        {
            JenisPublikasiResponse? JenisPublikasi = await JenisPublikasiApi.GetAsync(Guid.Parse(request.IndexJenisPublikasi));
            if (JenisPublikasi is null) {
                return Result.Failure<Guid>(InsentifErrors.NotFoundIndexJenisPublikasi(request.IndexJenisPublikasi));
            }

            Result<Domain.Insentif.Insentif> result = Domain.Insentif.Insentif.CreateInsentifJurnal(
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

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            InsentifPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
