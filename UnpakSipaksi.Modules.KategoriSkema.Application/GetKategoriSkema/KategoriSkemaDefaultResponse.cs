using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema
{
    public sealed record KategoriSkemaDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;

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
