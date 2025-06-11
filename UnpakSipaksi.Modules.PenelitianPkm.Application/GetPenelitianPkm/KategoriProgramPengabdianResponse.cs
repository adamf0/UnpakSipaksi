
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    public sealed record KategoriProgramPengabdianResponse
    {
        public string UuidKategoriProgramPengabdian { get; set; }
        public string Nama { get; set; }
        [JsonIgnore]
        public string Rule { get; set; } = default!;

        [JsonPropertyName("Rule")]
        public object? RuleObject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Rule))
                    return null;

                try
                {
                    return JsonSerializer.Deserialize<object>(Rule);
                }
                catch
                {
                    return Rule; // Jika gagal deserialisasi, kembalikan sebagai string biasa
                }
            }
        }
    }
}
