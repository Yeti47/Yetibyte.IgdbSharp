using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yetibyte.IgdbSharp.Data.Companies;

namespace Yetibyte.IgdbSharp.Serialization
{

    internal class IgdbResponseEntityDeserializer : IDeserializer
    {

        private readonly IContractResolver _contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };

        public T Deserialize<T>(IRestResponse response)
        {
            string content = response?.Content ?? string.Empty;

            T obj = JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
            {
                ContractResolver = _contractResolver
            });

            return obj;

        }

    }
}
