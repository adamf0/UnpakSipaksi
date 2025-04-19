using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.PublicApi
{
    public interface IJumlahKolaboratorPublikasBereputasiApi
    {
        Task<JumlahKolaboratorPublikasBereputasiResponse?> GetAsync(Guid JumlahKolaboratorPublikasBereputasiUuid, CancellationToken cancellationToken = default);
        Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default);
    }

}
