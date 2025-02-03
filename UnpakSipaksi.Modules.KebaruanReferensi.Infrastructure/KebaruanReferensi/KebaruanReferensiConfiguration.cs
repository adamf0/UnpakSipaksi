using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.KebaruanReferensi
{
    internal sealed class KebaruanReferensiConfiguration : IEntityTypeConfiguration<Domain.KebaruanReferensi.KebaruanReferensi>
    {
        public void Configure(EntityTypeBuilder<Domain.KebaruanReferensi.KebaruanReferensi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
