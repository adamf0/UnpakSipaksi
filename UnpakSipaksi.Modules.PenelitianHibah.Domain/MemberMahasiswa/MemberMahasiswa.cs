using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa
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
        public int PenelitianHibahId { get; private set; }
        [Column("bukti_mbkm")]
        public string BuktiMbkm { get; private set; }


        public static Result<MemberMahasiswa> Create(
          int PenelitianHibahId,
          string NPM
        )
        {
            var asset = new MemberMahasiswa
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = PenelitianHibahId, 
                NPM = NPM
            };

            asset.Raise(new MemberMahasiswaCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<MemberMahasiswa> Update(
          MemberMahasiswa prev,
          string NPM
        )
        {
            prev.NPM = NPM;

            return prev;
        }

        public static Result<MemberMahasiswa> UpdateMbkm(
          MemberMahasiswa? prev,
          string BuktiMbkm
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberMahasiswa>(PenelitianHibahErrors.EmptyData());
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
