using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public sealed partial class PenugasanReviewer
    {
        public sealed class PenugasanReviewerBuilder
        {
            private readonly PenugasanReviewer _akurasiPenelitian;
            private Result? _result;

            public PenugasanReviewerBuilder(PenugasanReviewer akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<PenugasanReviewer> Build()
            {
                return HasError ? Result.Failure<PenugasanReviewer>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public PenugasanReviewerBuilder ChangeNidn(string Nidn)
            {
                if (HasError) return this;

                if (!DomainValidator.IsValidNidn(Nidn))
                {
                    _result = Result.Failure<PenugasanReviewer>(PenugasanReviewerErrors.InvalidNidn());
                    return this;
                }

                _akurasiPenelitian.Nidn = Nidn;
                return this;
            }

            public PenugasanReviewerBuilder ChangeStatus(int status)
            {
                if (HasError) return this;

                if (status < 0 | status > 1)
                {
                    _result = Result.Failure<PenugasanReviewer>(PenugasanReviewerErrors.InvalidValueStatus());
                    return this;
                }

                _akurasiPenelitian.Status = status;
                return this;
            }

        }
    }
}
