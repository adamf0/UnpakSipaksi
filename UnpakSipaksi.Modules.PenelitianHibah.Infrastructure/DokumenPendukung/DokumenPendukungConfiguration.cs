using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenPendukung
{
    internal sealed class DokumenPendukungConfiguration : IEntityTypeConfiguration<Domain.DokumenPendukung.DokumenPendukung>
    {
        public void Configure(EntityTypeBuilder<Domain.DokumenPendukung.DokumenPendukung> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
