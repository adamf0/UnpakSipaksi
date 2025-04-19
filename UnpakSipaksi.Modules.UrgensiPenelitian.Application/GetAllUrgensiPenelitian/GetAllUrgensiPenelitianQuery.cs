using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetAllUrgensiPenelitian
{
    public sealed record GetAllUrgensiPenelitianQuery() : IQuery<List<UrgensiPenelitianResponse>>;
}
