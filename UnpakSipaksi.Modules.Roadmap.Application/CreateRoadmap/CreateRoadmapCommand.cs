using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap
{
    public sealed record CreateRoadmapCommand(
        string Nidn,
        string Link
    ) : ICommand<Guid>;
}
