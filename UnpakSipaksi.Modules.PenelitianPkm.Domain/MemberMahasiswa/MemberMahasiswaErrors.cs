using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa
{
    public static class MemberMahasiswaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("MemberMahasiswa.EmptyData", "data is not found");
        public static Error NotUnique(string NPM) =>
            Error.NotFound("MemberMahasiswa.NotUnique", $"NPM {NPM} is not unique");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("MemberMahasiswa.NotFound", $"Member hibah mahasiswa with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("MemberMahasiswa.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error InvalidUrlBuktiMbkm() =>
            Error.NotFound("MemberMahasiswa.InvalidUrlBuktiMbkm", "Bukti mbkm is invalid url");
        public static Error InvalidHostBuktiMbkm() =>
            Error.NotFound("MemberMahasiswa.InvalidHostBuktiMbkm", "Bukti mbkm is invalid host url");
        public static Error InvalidNpm() =>
            Error.NotFound("MemberMahasiswa.InvalidNpm", "Npm is invalid format");
        public static Error InvalidData() =>
            Error.NotFound("MemberMahasiswa.InvalidData", "Hibah penelitian is not match existing data");
    }
}
