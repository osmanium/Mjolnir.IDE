﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Util
{
    public static class SerializationHelper
    {
        public static string SerializeResponse<TObject>(TObject input)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = JsonSerializer.Create();

                    //Read the request
                    serializer.Serialize(jsonWriter, input);
                }
            }

            return sb.ToString();
        }

        public static TObject DeserizalizeRequest<TObject>(string input)
        {
            TObject request;
            using (StringReader sr = new StringReader(input))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(sr))
                {
                    var serializer = JsonSerializer.Create();

                    //Read the request
                    request = serializer.Deserialize<TObject>(jsonReader);
                }
            }

            return request;
        }

    }
}
