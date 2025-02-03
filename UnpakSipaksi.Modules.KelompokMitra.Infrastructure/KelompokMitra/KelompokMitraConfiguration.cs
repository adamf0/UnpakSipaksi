using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokMitra.Infrastructure.KelompokMitra
{
    internal sealed class KelompokMitraConfiguration : IEntityTypeConfiguration<Domain.KelompokMitra.KelompokMitra>
    {
        public void Configure(EntityTypeBuilder<Domain.KelompokMitra.KelompokMitra> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
