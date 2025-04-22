using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.Insentif
{
    internal sealed class InsentifConfiguration : IEntityTypeConfiguration<Domain.Insentif.Insentif>
    {
        public void Configure(EntityTypeBuilder<Domain.Insentif.Insentif> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
