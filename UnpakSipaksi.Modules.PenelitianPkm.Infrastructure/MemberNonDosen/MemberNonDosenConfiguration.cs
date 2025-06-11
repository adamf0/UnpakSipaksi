using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberNonDosen
{
    internal sealed class MemberNonDosenConfiguration : IEntityTypeConfiguration<Domain.MemberNonDosen.MemberNonDosen>
    {
        public void Configure(EntityTypeBuilder<Domain.MemberNonDosen.MemberNonDosen> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
