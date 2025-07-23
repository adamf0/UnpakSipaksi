using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public sealed partial class ModelFeasibilityStudys : Entity
    {
        private ModelFeasibilityStudys()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int BobotPDP { get; private set; } = 0;
        public int BobotTerapan { get; private set; } = 0;
        public int BobotKerjasama { get; private set; } = 0;
        public int BobotPenelitianDasar { get; private set; } = 0;
        public int Skor { get; private set; } = 0;

        public static ModelFeasibilityStudysBuilder Update(ModelFeasibilityStudys prev) => new ModelFeasibilityStudysBuilder(prev);

        public static Result<ModelFeasibilityStudys> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0)
            {
                return Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.InvalidValueSkor());
            }
            var asset = new ModelFeasibilityStudys
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new ModelFeasibilityStudysCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
