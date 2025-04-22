using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public enum JenisPublikasi
    {
        [EnumMember(Value = "prosiding")]
        Prosiding = 1,

        [EnumMember(Value = "jurnal")]
        Jurnal = 0
    }
}
