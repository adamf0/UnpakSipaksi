using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Domain
{
    public sealed class JenisLuaranCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
