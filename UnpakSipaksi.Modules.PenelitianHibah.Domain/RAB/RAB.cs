using System;
using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB
{
    public sealed partial class RAB : Entity
    {
        private RAB()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianHibahId { get; private set; }
        [Column("kelompok_rab")]
        public int? KelompokRabId { get; private set; }
        [Column("komponen")]
        public int? KomponenId { get; private set; }
        [Column("item")]
        public int? Item { get; private set; }
        [Column("satuan")]
        public int? SatuanId { get; private set; }
        [Column("harga_satuan")]
        public int? HargaSatuan { get; private set; }
        [Column("total")]
        public int? Total { get; private set; }


        public static Result<RAB> Create(
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          int? KelompokRabId,
          int? KomponenId,
          int? Item,
          int? SatuanId,
          int? HargaSatuan,
          int? Total
        )
        {
            if (existingPenelitianHibah == null) {
                return Result.Failure<RAB>((RABErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (Item != null && HargaSatuan!=null) {
                int calcTotal = (Item ?? 0) * (HargaSatuan??0);
                if (calcTotal != Total) {
                    return Result.Failure<RAB>(RABErrors.InvalidTotal());
                }
            }

            var asset = new RAB
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = existingPenelitianHibah?.Id ?? 0,
                KomponenId = KomponenId,
                Item = Item,
                SatuanId = SatuanId,
                HargaSatuan = HargaSatuan,
                Total = Total,
            };

            asset.Raise(new RABCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<RAB> Update(
          Guid uuid,
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          RAB? prev,
          int? KelompokRabId,
          int? KomponenId,
          int? Item,
          int? SatuanId,
          int? HargaSatuan,
          int? Total
        )
        {
            if (prev == null)
            {
                return Result.Failure<RAB>(RABErrors.NotFound(uuid));
            }
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<RAB>(RABErrors.NotFoundHibah(UuidPenelitianHibah));
            }
            if (prev.Id != existingPenelitianHibah.Id)
            {
                return Result.Failure<RAB>(RABErrors.InvalidData());
            }
            if (Item != null && HargaSatuan != null)
            {
                int calcTotal = (Item ?? 0) * (HargaSatuan ?? 0);
                if (calcTotal != Total)
                {
                    return Result.Failure<RAB>(RABErrors.InvalidTotal());
                }
            }

            prev.KomponenId = KomponenId;
            prev.Item = Item;
            prev.SatuanId = SatuanId;
            prev.HargaSatuan = HargaSatuan;
            prev.Total = Total;
           
            return prev;
        }
    }
}
