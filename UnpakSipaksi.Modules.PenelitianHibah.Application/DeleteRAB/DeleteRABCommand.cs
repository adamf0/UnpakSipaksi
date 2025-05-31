using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteRAB
{
    public sealed record DeleteRABCommand(
        string Uuid,
        string UuidPenelitianHibah
    ) : ICommand;
}
