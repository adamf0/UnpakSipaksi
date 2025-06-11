using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.IndikatorCapaian
{
    internal sealed class IndikatorCapaianConfiguration : IEntityTypeConfiguration<Domain.IndikatorCapaian>
    {
        public void Configure(EntityTypeBuilder<Domain.IndikatorCapaian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
