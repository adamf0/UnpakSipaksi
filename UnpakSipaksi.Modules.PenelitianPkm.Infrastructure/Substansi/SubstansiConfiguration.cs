using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Substansi
{
    internal sealed class SubstansiConfiguration : IEntityTypeConfiguration<Domain.Substansi.Substansi>
    {
        public void Configure(EntityTypeBuilder<Domain.Substansi.Substansi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
