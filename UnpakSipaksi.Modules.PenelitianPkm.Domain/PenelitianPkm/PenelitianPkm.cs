using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm
{
    public sealed partial class PenelitianPkm : Entity
    {
        private PenelitianPkm()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("NIDN")]
        public string NIDN { get; private set; }

        [Column("judul")]
        public string Judul { get; private set; }

        [Column("id_kategori_program_pengabdian")]
        public int? KategoriProgramPengabdianId { get; private set; }

        [Column("id_fokus_pengabdian")]
        public int? FokusPenelitianId { get; private set; }

        [Column("id_rirn")]
        public int? RirnId { get; private set; }

        [Column("id_rumpun_ilmu1")]
        public int? RumpunIlmu1Id { get; private set; }

        [Column("id_rumpun_ilmu2")]
        public int? RumpunIlmu2Id { get; private set; }

        [Column("id_rumpun_ilmu3")]
        public int? RumpunIlmu3Id { get; private set; }

        [Column("tahun_pengajuan")]
        public DateTime TahunPengajuan { get; private set; }

        [Column("status")]
        public string Status { get; private set; }

        [Column("type")]
        public string? Type { get; private set; }

        [Column("catatan")]
        public string? Catatan { get; private set; }

        //[Column("catatan_tolak")]
        //public string? CatatanTolak { get; private set; }


        public static async Task<Result<PenelitianPkm>> Create(
          IPenelitianPkmRepository penelitianHibahRepository,
          string NIDN,
          string TahunPengajuan,
          string Judul
        )
        {
            bool isUnique = await penelitianHibahRepository.HasUniqueDataAsync(
                null,
                NIDN,
                Judul
            );
            if (!isUnique)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.NotUnique(NIDN, Judul));
            }
            if (!DomainValidator.IsValidNidn(NIDN))
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidNidn());
            }

            var validTanggal = DateTime.TryParseExact(TahunPengajuan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);
            if (!validTanggal)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidTahunPengajuan());
            }

            var asset = new PenelitianPkm
            {
                Uuid = Guid.NewGuid(),
                NIDN = NIDN,
                Judul = Judul,
                TahunPengajuan = parsedTanggal,
                Status = "Draf"
            };

            asset.Raise(new PenelitianPkmCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public async static Task<Result<PenelitianPkm>> Update(
          PenelitianPkm prev,
          IPenelitianPkmRepository penelitianHibahRepository,
          string TahunPengajuan,
          string Judul
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.NotFound(prev?.Uuid ?? Guid.Empty));
            }

            bool isUnique = await penelitianHibahRepository.HasUniqueDataAsync(
                prev!.Uuid,
                prev!.NIDN,
                prev!.Judul
            );
            if (!isUnique)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.NotUnique(prev!.NIDN, prev!.Judul));
            }

            var validTanggal = DateTime.TryParseExact(TahunPengajuan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);
            if (!validTanggal)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidTahunPengajuan());
            }

            prev.Judul = Judul;
            prev.TahunPengajuan = parsedTanggal;

            return prev;
        }

        public static Result<PenelitianPkm> UpdateKategoriProgramPengabdian(
          PenelitianPkm? prev,
          int KategoriProgramPengabdianId
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.EmptyData());
            }
            if (KategoriProgramPengabdianId <= 0) {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidKategoriPengabdian());
            }
            prev.KategoriProgramPengabdianId = KategoriProgramPengabdianId;

            return prev;
        }

        public static Result<PenelitianPkm> UpdateProgramPengabdian(
          PenelitianPkm? prev,
          int? FokusPenelitianId,
          int? RirnId
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.EmptyData());
            }
            if (FokusPenelitianId <= 0 && RirnId <= 0)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidProgramPengabdian());
            }

            prev.FokusPenelitianId = FokusPenelitianId==0? null: FokusPenelitianId;
            prev.RirnId = RirnId <= 0 ? null : RirnId;

            return prev;
        }

        public static Result<PenelitianPkm> UpdateRumpunIlmu(
          PenelitianPkm? prev,
          int? RumpunIlmu1Id,
          int? RumpunIlmu2Id,
          int? RumpunIlmu3Id
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.EmptyData());
            }

            prev.RumpunIlmu1Id = RumpunIlmu1Id;
            prev.RumpunIlmu2Id = RumpunIlmu2Id;
            prev.RumpunIlmu3Id = RumpunIlmu3Id;

            return prev;
        }

        public static Result<PenelitianPkm> UpdateStatus(
          PenelitianPkm? prev,
          string Status
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.EmptyData());
            }
            if (!EnumExtensions.GetAllEnumStrings<StatusPengajuan>().Contains(Status, StringComparer.OrdinalIgnoreCase))
            {
                return Result.Failure<PenelitianPkm>(PenelitianPkmErrors.InvalidStatus());
            }
            prev.Status = Status;

            return prev;
        }
    }
}
