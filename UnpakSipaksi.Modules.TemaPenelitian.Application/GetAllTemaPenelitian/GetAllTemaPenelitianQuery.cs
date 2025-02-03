using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.GetAllTemaPenelitian
{
    public sealed record GetAllTemaPenelitianQuery() : IQuery<List<TemaPenelitianResponse>>;
}
