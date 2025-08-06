using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen
{
    public sealed partial class MemberDosen : Entity
    {
        private MemberDosen()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("NIDN")]
        public string NIDN { get; private set; }
        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("status")]
        public int Status { get; private set; }


        public static Result<MemberDosen> Create(
          int checkData,
          int PenelitianPkmId,
          string NIDN
        )
        {
            if (PenelitianPkmId <= 0)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.NotFoundHibah());
            }
            if (checkData > 0)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.NotUnique(NIDN));
            }

            var asset = new MemberDosen
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = PenelitianPkmId,
                NIDN = NIDN,
                Status = 0,
            };

            asset.Raise(new MemberDosenCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<MemberDosen> Update(
          int checkData,
          MemberDosen prev,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah,
          string Nidn
        )
        {
            if (checkData > 0)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.NotUnique(Nidn));
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.InvalidData());

            }
            prev.NIDN = Nidn;

            return prev;
        }

        public static Result<MemberDosen> Approve(
            MemberDosen? prev,
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberDosen>(PenelitianPkmErrors.EmptyData());
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.InvalidData());
            }

            prev.Status = 1;

            return prev;
        }

        public static Result<MemberDosen> Reject(
            MemberDosen? prev,
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianHibah
        )
        {
            if (prev is null)
            {
                return Result.Failure<MemberDosen>(PenelitianPkmErrors.EmptyData());
            }
            if (prev?.PenelitianPkmId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<MemberDosen>(MemberDosenErrors.InvalidData());
            }

            prev.Status = -1;

            return prev;
        }
    }
}
