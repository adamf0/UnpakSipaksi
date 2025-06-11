using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PenelitianPkm
{
    internal sealed class PenelitianPkmConfiguration : IEntityTypeConfiguration<Domain.PenelitianPkm.PenelitianPkm>
    {
        public void Configure(EntityTypeBuilder<Domain.PenelitianPkm.PenelitianPkm> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
