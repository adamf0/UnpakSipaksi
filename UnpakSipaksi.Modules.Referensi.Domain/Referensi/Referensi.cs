using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Referensi.Domain.Referensi
{
    public sealed partial class Referensi : Entity
    {
        private Referensi()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int KebaruanReferensiId { get; private set; } = 0;
        public int RelevansiKualitasReferensiId { get; private set; } = 0;
        public int Nilai { get; private set; } = 0;

        public static ReferensiBuilder Update(Referensi prev) => new ReferensiBuilder(prev);

        public static Result<Referensi> Create(
        string Nama,
        int KebaruanReferensiId,
        int RelevansiKualitasReferensiId,
        int Nilai
        )
        {
            if (KebaruanReferensiId <= 0)
                return Result.Failure<Referensi>(ReferensiErrors.NotFoundKebaruanReferensiId());

            if (RelevansiKualitasReferensiId <= 0)
                return Result.Failure<Referensi>(ReferensiErrors.NotFoundRelevansiKualitasReferensiId());

            if(Nilai < 0)
                return Result.Failure<Referensi>(ReferensiErrors.InvalidValueNilai());

            var asset = new Referensi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                KebaruanReferensiId  = KebaruanReferensiId,
                RelevansiKualitasReferensiId = RelevansiKualitasReferensiId,
                Nilai = Nilai
            };

            asset.Raise(new ReferensiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
