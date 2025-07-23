using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

//[PR] perlu validasi panjang max nidn di domain
namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public sealed partial class PenugasanReviewer : Entity
    {
        private PenugasanReviewer()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nidn { get; private set; } = null!;
        public int Status { get; private set; } = 0;

        public static PenugasanReviewerBuilder Update(PenugasanReviewer prev) => new PenugasanReviewerBuilder(prev);

        public static Result<PenugasanReviewer> Create(
        string Nidn,
        int Status
        )
        {
            if (Status < 0 | Status > 1) {
                return Result.Failure<PenugasanReviewer>(PenugasanReviewerErrors.InvalidValueStatus());
            }
            var asset = new PenugasanReviewer
            {
                Uuid = Guid.NewGuid(),
                Nidn = Nidn,
                Status = Status
            };

            asset.Raise(new PenugasanReviewerCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
