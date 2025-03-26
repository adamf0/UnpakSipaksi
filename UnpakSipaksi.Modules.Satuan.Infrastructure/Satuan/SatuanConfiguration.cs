using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Satuan.Infrastructure.Satuan
{
    internal sealed class SatuanConfiguration : IEntityTypeConfiguration<Domain.Satuan.Satuan>
    {
        public void Configure(EntityTypeBuilder<Domain.Satuan.Satuan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
