using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta
{
    public sealed partial class AuthorSinta : Entity
    {
        private AuthorSinta()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("NIDN")]
        public string Nidn { get; private set; } = null!;

        [Column("author_id")]
        public string? AuthorId { get; private set; } = null;

        public int Score { get; private set; } = 0;

        public static AuthorSintaBuilder Update(AuthorSinta prev) => new AuthorSintaBuilder(prev);

        public static Result<AuthorSinta> Create(
        string Nidn,
        string? AuthorId,
        int Score
        )
        {
            if (string.IsNullOrEmpty(AuthorId) || AuthorId.Length != 7) {
                return Result.Failure<AuthorSinta>(AuthorSintaErrors.InvalidAuthorId());
            }
            if (Score <= 0)
            {
                return Result.Failure<AuthorSinta>(AuthorSintaErrors.InvalidSkor());
            }

            var asset = new AuthorSinta
            {
                Uuid = Guid.NewGuid(),
                Nidn = Nidn,
                AuthorId = AuthorId,
                Score = Score
            };

            asset.Raise(new AuthorSintaCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
