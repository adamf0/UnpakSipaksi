using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa
{
    public sealed partial class MemberMahasiswa : Entity
    {
        private MemberMahasiswa()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("nim")]
        public string NPM { get; private set; }
        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("bukti_mbkm")]
        public string BuktiMbkm { get; private set; }


        public static Result<MemberMahasiswa> Create(
            int checkData,
            int PenelitianPkmId,
            string NPM
        )
        {
            if (checkData > 0)
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.NotUnique(NPM));
            }

            if (!DomainValidator.IsValidNPM(NPM))
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidNpm());
            }

            var asset = new MemberMahasiswa
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = PenelitianPkmId,
                NPM = NPM
            };

            asset.Raise(new MemberMahasiswaCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<MemberMahasiswa> Update(
            int checkData,
            MemberMahasiswa prev,
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah,
            string NPM
        )
        {
            if (checkData > 0)
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.NotUnique(NPM));
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidData());
            }
            if (!DomainValidator.IsValidNPM(NPM))
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidNpm());
            }

            prev.NPM = NPM;

            return prev;
        }

        public static Result<MemberMahasiswa> UpdateMbkm(
          MemberMahasiswa? prev,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah,
          string BuktiMbkm
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberMahasiswa>(PenelitianPkmErrors.EmptyData());
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidData());
            }
            if (!Uri.TryCreate(BuktiMbkm, UriKind.Absolute, out var uriResult) || uriResult.Scheme != Uri.UriSchemeHttps)
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidUrlBuktiMbkm());
            }

            var allowedHost = "drive.google.com";
            if (!string.Equals(uriResult.Host, allowedHost, StringComparison.OrdinalIgnoreCase))
            {
                return Result.Failure<MemberMahasiswa>(MemberMahasiswaErrors.InvalidHostBuktiMbkm());
            }

            prev.BuktiMbkm = BuktiMbkm;

            return prev;
        }
    }
}
