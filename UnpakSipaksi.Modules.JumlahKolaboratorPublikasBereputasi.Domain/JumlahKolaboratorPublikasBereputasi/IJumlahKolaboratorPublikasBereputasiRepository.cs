using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi
{
    public interface IJumlahKolaboratorPublikasBereputasiRepository
    {
        void Insert(JumlahKolaboratorPublikasBereputasi akurasiPenelitian);
        Task<JumlahKolaboratorPublikasBereputasi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(JumlahKolaboratorPublikasBereputasi akurasiPenelitian);
    }
}
