using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiLppm
{
    internal sealed class VerifikasiLppmConfiguration : IEntityTypeConfiguration<Domain.VerifikasiLppm.VerifikasiLppm>
    {
        public void Configure(EntityTypeBuilder<Domain.VerifikasiLppm.VerifikasiLppm> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
