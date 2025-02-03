using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.KategoriMitraPenelitian
{
    internal sealed class KategoriMitraPenelitianConfiguration : IEntityTypeConfiguration<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
