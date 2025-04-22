
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiFakultas
{
    internal sealed class VerifikasiFakultasConfiguration : IEntityTypeConfiguration<Domain.VerifikasiFakultas.VerifikasiFakultas>
    {
        public void Configure(EntityTypeBuilder<Domain.VerifikasiFakultas.VerifikasiFakultas> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
