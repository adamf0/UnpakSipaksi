using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public enum Peran
    {
        [EnumMember(Value = "first author")]
        FirstAuthor,

        [EnumMember(Value = "co author")]
        CoAuthor
    }
}
