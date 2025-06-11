using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.RAB
{
    internal sealed class RABConfiguration : IEntityTypeConfiguration<Domain.RAB.RAB>
    {
        public void Configure(EntityTypeBuilder<Domain.RAB.RAB> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
