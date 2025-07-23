using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta
{
    public sealed partial class AuthorSinta
    {
        public sealed class AuthorSintaBuilder
        {
            private readonly AuthorSinta _akurasiPenelitian;
            private Result? _result;

            public AuthorSintaBuilder(AuthorSinta akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<AuthorSinta> Build()
            {
                return HasError ? Result.Failure<AuthorSinta>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public AuthorSintaBuilder ChangeNIDN(string Nidn)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<AuthorSinta>(AuthorSintaErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nidn = Nidn;
                return this;
            }

            public AuthorSintaBuilder ChangeAuthorId(string? authorId)
            {
                if (HasError) return this;

                if (string.IsNullOrEmpty(authorId) || authorId.Length != 7)
                {
                    _result = Result.Failure<AuthorSinta>(AuthorSintaErrors.InvalidAuthorId());
                }

                _akurasiPenelitian.AuthorId = authorId;
                return this;
            }

            public AuthorSintaBuilder ChangeScore(int score)
            {
                if (HasError) return this;

                if (score <= 0)
                {
                    
                    _result = Result.Failure<AuthorSinta>(AuthorSintaErrors.InvalidSkor());
                }

                _akurasiPenelitian.Score = score;
                return this;
            }

        }
    }
}
