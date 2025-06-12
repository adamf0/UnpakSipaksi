using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenMitra
{
    internal sealed class DokumenMitraConfiguration : IEntityTypeConfiguration<Domain.DokumenMitra.DokumenMitra>
    {
        public void Configure(EntityTypeBuilder<Domain.DokumenMitra.DokumenMitra> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
