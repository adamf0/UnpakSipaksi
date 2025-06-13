using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm
{
    public static class EntityExtensions
    {
        public static Dictionary<string, string> GetContentAttributes(this object entity)
        {
            var excludedProperties = new HashSet<string>
            {
                "Id",
                "Uuid",
                "Level",
                "Keputusan",
                "Komentar"
            };

            return entity.GetType()
                         .GetProperties()
                         .Where(p => p.PropertyType == typeof(string) && !excludedProperties.Contains(p.Name))
                         .ToDictionary(
                             prop => prop.Name,
                             prop => prop.GetValue(entity)?.ToString() ?? string.Empty
                         );
        }
    }
}
