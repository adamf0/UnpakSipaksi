using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.InovasiPemecahanMasalah
{
    internal sealed class InovasiPemecahanMasalahConfiguration : IEntityTypeConfiguration<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah>
    {
        public void Configure(EntityTypeBuilder<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
