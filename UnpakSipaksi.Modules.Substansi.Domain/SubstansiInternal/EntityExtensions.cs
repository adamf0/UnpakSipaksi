using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal
{
    public static class EntityExtensions
    {
        public static Dictionary<string, string> GetContentAttributes(this object entity)
        {
            var excludedProperties = new HashSet<string>
            {
                "Id",
                "Uuid",
                "Komentar",
                "ReviewerInternal",
                "ReviewerExternal",
                "TanggalMulai",
                "TanggalBerakhir",
            };

            return entity.GetType()
                         .GetProperties()
                         .Where(p => p.PropertyType == typeof(int) && !excludedProperties.Contains(p.Name))
                         .ToDictionary(
                             prop => prop.Name,
                             prop => prop.GetValue(entity)?.ToString() ?? string.Empty
                         );
        }
    }
}
