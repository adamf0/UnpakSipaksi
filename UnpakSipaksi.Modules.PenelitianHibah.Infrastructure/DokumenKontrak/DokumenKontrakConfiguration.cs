using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.DokumenKontrak
{
    internal sealed class DokumenKontrakConfiguration : IEntityTypeConfiguration<Domain.DokumenKontrak.DokumenKontrak>
    {
        public void Configure(EntityTypeBuilder<Domain.DokumenKontrak.DokumenKontrak> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
