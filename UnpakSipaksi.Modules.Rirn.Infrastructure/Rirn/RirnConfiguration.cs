using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Rirn.Infrastructure.Rirn
{
    internal sealed class RirnConfiguration : IEntityTypeConfiguration<Domain.Rirn.Rirn>
    {
        public void Configure(EntityTypeBuilder<Domain.Rirn.Rirn> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
