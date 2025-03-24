using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PrioritasRiset
{
    internal sealed class PrioritasRisetConfiguration : IEntityTypeConfiguration<Domain.PrioritasRiset.PrioritasRiset>
    {
        public void Configure(EntityTypeBuilder<Domain.PrioritasRiset.PrioritasRiset> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
