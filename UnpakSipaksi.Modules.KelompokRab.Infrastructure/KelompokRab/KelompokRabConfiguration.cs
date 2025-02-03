using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokRab.Infrastructure.KelompokRab
{
    internal sealed class KelompokRabConfiguration : IEntityTypeConfiguration<Domain.KelompokRab.KelompokRab>
    {
        public void Configure(EntityTypeBuilder<Domain.KelompokRab.KelompokRab> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
