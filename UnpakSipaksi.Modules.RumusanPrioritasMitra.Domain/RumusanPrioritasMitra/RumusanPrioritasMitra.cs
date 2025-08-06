using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public sealed partial class RumusanPrioritasMitra : Entity
    {
        private RumusanPrioritasMitra()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static RumusanPrioritasMitraBuilder Update(RumusanPrioritasMitra prev) => new RumusanPrioritasMitraBuilder(prev);

        public static Result<RumusanPrioritasMitra> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<RumusanPrioritasMitra>(RumusanPrioritasMitraErrors.InvalidValueNilai());
            }
            var asset = new RumusanPrioritasMitra
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new RumusanPrioritasMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
