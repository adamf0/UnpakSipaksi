using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen
{
    public static class MemberDosenErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("MemberDosen.EmptyData", "data is not found");
        public static Error NotUnique(string NIDN) =>
            Error.NotFound("MemberDosen.NotUnique", $"Nidn {NIDN} is not unique");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("MemberDosen.NotFound", $"Member hibah dosen with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("MemberDosen.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error NotFoundHibah() =>
            Error.NotFound("Luaran.NotFoundHibah", "Penelitian hibah not found");
        public static Error InvalidData() =>
            Error.NotFound("Luaran.InvalidData", "Hibah penelitian is not match existing data");

    }
}
