using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian
{
    public sealed record GetUrgensiPenelitianQuery(Guid UrgensiPenelitianUuid) : IQuery<UrgensiPenelitianResponse>;
}
