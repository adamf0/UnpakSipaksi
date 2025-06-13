using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiInternal
{
    internal sealed class AdministrasiInternalConfiguration : IEntityTypeConfiguration<Domain.AdministrasiInternal.AdministrasiInternal>
    {
        public void Configure(EntityTypeBuilder<Domain.AdministrasiInternal.AdministrasiInternal> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
