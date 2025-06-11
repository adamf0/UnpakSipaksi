
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    public sealed record KategoriProgramPengabdianDefaultResponse(
        string? Id,
        string? Uuid,
        string? Nama
    )
    {
        [JsonIgnore]
        public string Rule { get; init; } = string.Empty;

        [JsonPropertyName("Rule")]
        public object? RuleObject =>
            string.IsNullOrWhiteSpace(Rule)
                ? null
                : TryDeserializeJson(Rule) ?? Rule;

        private static object? TryDeserializeJson(string input)
        {
            try
            {
                return JsonSerializer.Deserialize<object>(input);
            }
            catch
            {
                return null;
            }
        }
    }
}
