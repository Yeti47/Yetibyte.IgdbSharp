using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Text.RegularExpressions;

namespace Yetibyte.IgdbSharp.Serialization
{



    internal class IgdbResponseDeserializer : IDeserializer
    {

        #region Constants

        private const string INT_ARRAY_REGEX = @"\[\s*\d+\s*\]";

        #endregion

        #region Fields

        private static readonly JsonDeserializer _defaultDeserializer = new JsonDeserializer();

        #endregion

        #region Methods

        /// <summary>
        /// Deserializes the data in the given response into a strongly-typed object of the target type.
        /// This implementation of <see cref="IDeserializer"/> makes sure to replace integer/long arrays that only contain a single item with arrays of simple objects with an "id" property.
        /// <para>
        /// Example: <br/>
        /// [ 47 ] => [ { "id": 47 } ]
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type into which the response data should be deserialized.</typeparam>
        /// <param name="response">The response to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public T Deserialize<T>(IRestResponse response)
        {
            string content = response?.Content;

            if(!string.IsNullOrWhiteSpace(content))
            {

                foreach(Match match in Regex.Matches(content, INT_ARRAY_REGEX))
                {
                    if (!match.Success)
                        continue;

                    content = content.Replace(match.Value, 
                        "[ { \"id\": " 
                            + match.Value.Replace("[", string.Empty).Replace("]", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty) 
                            + " } ]"
                    );
                }

                response.Content = content;

            }

            //var originalStrat = SimpleJson.CurrentJsonSerializerStrategy;
            //SimpleJson.CurrentJsonSerializerStrategy = new SerializerStrategy();

            T obj = _defaultDeserializer.Deserialize<T>(response);

            //T obj = SimpleJson.DeserializeObject<T>(content);

            //SimpleJson.CurrentJsonSerializerStrategy = originalStrat;

            return obj;

        }


        #endregion

    }
}
