using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Administrasi.Domain
{
    public enum Options
    {
        [EnumMember(Value = "tidak ada")]
        TidakAda,

        [EnumMember(Value = "ada tapi tidak sesuai")]
        AdaTapiTidakSesuai,

        [EnumMember(Value = "ada dan sesuai")]
        AdaSesuai
    }
}
