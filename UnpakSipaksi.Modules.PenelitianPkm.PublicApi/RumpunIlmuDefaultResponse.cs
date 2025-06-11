

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.PublicApi
{
    public sealed record RumpunIlmuDefaultResponse(
        string IdRumpunIlmu1,
        string UuidRumpunIlmu1,
        string NamaRumpunIlmu1,
        string IdRumpunIlmu2,
        string UuidRumpunIlmu2,
        string NamaRumpunIlmu2,
        string IdRumpunIlmu3,
        string UuidRumpunIlmu3,
        string NamaRumpunIlmu3
    );
}
