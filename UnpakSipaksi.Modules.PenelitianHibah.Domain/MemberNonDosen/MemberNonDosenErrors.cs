using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen
{
    public static class MemberNonDosenErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("MemberNonDosen.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("MemberNonDosen.NotFound", $"Member hibah dosen with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("MemberNonDosen.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error NotFoundHibah() =>
            Error.NotFound("MemberNonDosen.NotFoundHibah", "Penelitian hibah not found");
        public static Error InvalidData() =>
            Error.NotFound("MemberNonDosen.InvalidData", "Hibah penelitian is not match existing data");
    }
}
