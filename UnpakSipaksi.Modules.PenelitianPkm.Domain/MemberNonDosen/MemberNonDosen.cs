using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen
{
    public sealed partial class MemberNonDosen : Entity
    {
        private MemberNonDosen()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("nomorIdentitas")]
        public string? NomorIdentitas { get; private set; }
        [Column("nama")]
        public string? Nama { get; private set; }
        [Column("afiliasi")]
        public string? Afiliasi { get; private set; }


        public static Result<MemberNonDosen> Create(
          int PenelitianPkmId,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
        )
        {
            if (PenelitianPkmId <= 0) {
                return Result.Failure<MemberNonDosen>(MemberNonDosenErrors.NotFoundHibah());
            }
            var asset = new MemberNonDosen
            {
                Uuid = Guid.NewGuid(),
                NomorIdentitas = NomorIdentitas,
                PenelitianPkmId = PenelitianPkmId,
                Nama = Nama,
                Afiliasi = Afiliasi,
            };

            asset.Raise(new MemberNonDosenCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<MemberNonDosen> Update(
          MemberNonDosen? prev,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberNonDosen>(PenelitianPkmErrors.EmptyData());
            }
            if (existingPenelitianHibah?.Id <= 0)
            {
                return Result.Failure<MemberNonDosen>(MemberNonDosenErrors.NotFoundHibah());
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberNonDosen>(MemberNonDosenErrors.InvalidData());
            }

            prev.NomorIdentitas = NomorIdentitas;
            prev.Nama = Nama;
            prev.Afiliasi = Afiliasi;

            return prev;
        }
    }
}
