using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.UpdateKategoriLuaran
{
    public sealed record UpdateKategoriLuaranCommand(
        string Uuid,
        string UuidKategori,
        string Nama,
        string Status
    ) : ICommand;
}
