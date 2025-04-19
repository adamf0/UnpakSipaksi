using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Referensi.Infrastructure.Referensi
{
    internal sealed class ReferensiConfiguration : IEntityTypeConfiguration<Domain.Referensi.Referensi>
    {
        public void Configure(EntityTypeBuilder<Domain.Referensi.Referensi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
