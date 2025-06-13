using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiPkm
{
    internal sealed class AdministrasiPkmConfiguration : IEntityTypeConfiguration<Domain.AdministrasiPkm.AdministrasiPkm>
    {
        public void Configure(EntityTypeBuilder<Domain.AdministrasiPkm.AdministrasiPkm> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
