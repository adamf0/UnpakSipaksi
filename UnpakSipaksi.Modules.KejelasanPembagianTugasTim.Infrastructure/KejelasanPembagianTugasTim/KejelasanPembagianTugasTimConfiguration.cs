using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.KejelasanPembagianTugasTim
{
    internal sealed class KejelasanPembagianTugasTimConfiguration : IEntityTypeConfiguration<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim>
    {
        public void Configure(EntityTypeBuilder<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
