using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen
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
        public int PenelitianHibahId { get; private set; }
        [Column("nomorIdentitas")]
        public string? NomorIdentitas { get; private set; }
        [Column("nama")]
        public string? Nama{ get; private set; }
        [Column("afiliasi")]
        public string? Afiliasi { get; private set; }


        public static Result<MemberNonDosen> Create(
          int PenelitianHibahId,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
        )
        {
            if (PenelitianHibahId <= 0) {
                return Result.Failure<MemberNonDosen>(MemberNonDosenErrors.NotFoundHibah());
            }
            var asset = new MemberNonDosen
            {
                Uuid = Guid.NewGuid(),
                NomorIdentitas = NomorIdentitas,
                PenelitianHibahId = PenelitianHibahId,
                Nama = Nama,
                Afiliasi = Afiliasi,
            };

            asset.Raise(new MemberNonDosenCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<MemberNonDosen> Update(
          MemberNonDosen? prev,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberNonDosen>(PenelitianHibahErrors.EmptyData());
            }
            if (existingPenelitianHibah?.Id <= 0)
            {
                return Result.Failure<MemberNonDosen>(MemberNonDosenErrors.NotFoundHibah());
            }
            if (prev?.PenelitianHibahId != existingPenelitianHibah?.Id)
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
