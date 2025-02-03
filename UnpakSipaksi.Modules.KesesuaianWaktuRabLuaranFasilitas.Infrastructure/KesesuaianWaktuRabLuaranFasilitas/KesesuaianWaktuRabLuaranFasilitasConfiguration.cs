using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.KesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class KesesuaianWaktuRabLuaranFasilitasConfiguration : IEntityTypeConfiguration<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas>
    {
        public void Configure(EntityTypeBuilder<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
