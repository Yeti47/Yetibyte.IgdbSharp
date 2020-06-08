using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.IgdbSharp.Data.Common;
using Yetibyte.IgdbSharp.Data.GameEngines;
using Yetibyte.IgdbSharp.Queries;
using Yetibyte.IgdbSharp.Serialization;

namespace Yetibyte.IgdbSharp.Client
{

    public class IgdbClient
    {

        public const string BASE_URL = "https://api-v3.igdb.com";

        private const string HEADER_NAME_USER_KEY = "user-key";
        private const string HEADER_NAME_ACCEPT = "Accept";

        private const string RESOURCE_GAME_ENGINES = "game_engines";

        private const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        private const string CONTENT_TYPE_TEXT_PLAIN = "text/plain";

        private const string ERR_MSG_UNKNOWN = "An unknown error has occurred.";
        private const string ERR_MSG_BAD_STATUS = "A server error occurred while handling the request.";

        private readonly RestClient _restClient;

        /// <summary>
        /// The API key to use for authentication.
        /// </summary>
        public string ApiKey { get; }

        private Method HttpRetrievalMethod => Method.POST;

        public IgdbClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("An API key must be provided.", nameof(apiKey));
            }

            ApiKey = apiKey;
            _restClient = new RestClient(BASE_URL);
            _restClient.AddDefaultHeader(HEADER_NAME_USER_KEY, ApiKey);
            _restClient.AddDefaultHeader(HEADER_NAME_ACCEPT, CONTENT_TYPE_APPLICATION_JSON);
            //_restClient.AddHandler(CONTENT_TYPE_APPLICATION_JSON, () => new IgdbResponseDeserializer());
            _restClient.AddHandler(CONTENT_TYPE_APPLICATION_JSON, () => new IgdbResponseEntityDeserializer());

        }

        protected IRestRequest PrepareRequest(string resourceUrl, IApiQuery query = null)
        {
            query = query ?? new ApiQuery();

            var request = new RestRequest(resourceUrl, HttpRetrievalMethod, DataFormat.Json);

            request.AddParameter(CONTENT_TYPE_TEXT_PLAIN, query.GetQueryString(), ParameterType.RequestBody);

            return request;
        }

        protected Exception BuildExceptionForResponse(IRestResponse response)
        {
            if (response is null)
                return new IgdbClientException(ERR_MSG_UNKNOWN);

            if(response.StatusCode != 0 
                && response.StatusCode != System.Net.HttpStatusCode.OK 
                && response.StatusCode != System.Net.HttpStatusCode.Accepted 
                && response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                JsonDeserializer jsonDeserializer = new JsonDeserializer();

                ErrorInformation errorInformation = jsonDeserializer.Deserialize<List<ErrorInformation>>(response)?.FirstOrDefault();

                Exception ex = errorInformation != null && errorInformation.Status != 0 
                    ? new IgdbClientBadStatusException(errorInformation) 
                    : new IgdbClientBadStatusException(ERR_MSG_BAD_STATUS);
                
                return ex;

            }

            return response.IsSuccessful 
                ? null 
                : new IgdbClientException(response.ErrorMessage, response.ErrorException);

        }

        public IEnumerable<GameEngine> GetGameEngines(IApiQuery query = null)
        {
            IRestRequest req = PrepareRequest(RESOURCE_GAME_ENGINES, query);

            var gameEnginesResponse = _restClient.Post<List<GameEngine>>(req);

            Exception exception = BuildExceptionForResponse(gameEnginesResponse);

            if (exception != null)
                throw exception;
            
            return gameEnginesResponse?.Data;
        }

        public IEnumerable<GameEngine> GetGameEngines(Action<ApiQueryBuilder> queryFactory)
        {
            if (queryFactory == null)
                return GetGameEngines();

            ApiQueryBuilder builder = new ApiQueryBuilder();
            queryFactory.Invoke(builder);

            ApiQuery query = builder.Build();

            return GetGameEngines(query);
        }

        public async Task<IEnumerable<GameEngine>> GetGameEnginesAsync(IApiQuery query = null)
        {
            IRestRequest req = PrepareRequest(RESOURCE_GAME_ENGINES, query);

            var response = await _restClient.ExecutePostAsync<List<GameEngine>>(req);

            Exception exception = BuildExceptionForResponse(response);

            if (exception != null)
                throw exception;

            return response?.Data;
        }


    }
}
