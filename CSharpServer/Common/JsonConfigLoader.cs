using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using asset

namespace Common
{
    public sealed class JsonConfigLoader
    {
        public static T LoadConfig<T>(String path)
        {
            if (File.Exists(path) == false)
                return default(T);

            var jsonparse = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(jsonparse);
        }
    }
}
