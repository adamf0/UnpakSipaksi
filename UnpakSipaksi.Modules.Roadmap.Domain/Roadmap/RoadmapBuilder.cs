using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Roadmap.Domain.Roadmap
{
    public sealed partial class Roadmap
    {
        public sealed class RoadmapBuilder
        {
            private readonly Roadmap _akurasiPenelitian;
            private Result? _result;

            public RoadmapBuilder(Roadmap akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Roadmap> Build()
            {
                return HasError ? Result.Failure<Roadmap>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RoadmapBuilder ChangeNidn(string Nidn)
            {
                if (HasError) return this;

                if (!DomainValidator.IsValidNidn(Nidn))
                {
                    _result = Result.Failure<Roadmap>(RoadmapErrors.InvalidNidn());
                    return this;
                }

                _akurasiPenelitian.Nidn = Nidn;
                return this;
            }

            public RoadmapBuilder ChangeLink(string Link)
            {
                if (HasError) return this;

                if (!DomainValidator.IsValidGoogleDriveUrl(Link, "drive.google.com"))
                {
                    _result = Result.Failure<Roadmap>(RoadmapErrors.InvalidLink());
                    return this;
                }

                _akurasiPenelitian.Link = Link;
                return this;
            }

        }
    }
}
