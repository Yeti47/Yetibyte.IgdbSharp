using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Yetibyte.IgdbSharp.Queries
{

    public class ApiQuery : IApiQuery
    {
        #region Constants

        public const string WILD_CARD_FIELD = "*";
        private const string SORT_MODE_ASCENDING = "asc";
        private const string SORT_MODE_DESCENDING = "desc";

        private const string PARAMETER_NAME_FIELDS = "fields";
        private const string PARAMETER_NAME_EXCLUDE = "exclude";
        private const string PARAMETER_NAME_WHERE = "where";
        private const string PARAMETER_NAME_LIMIT = "limit";
        private const string PARAMETER_NAME_OFFSET = "offset";
        private const string PARAMETER_NAME_SORT = "sort";
        private const string PARAMETER_NAME_SEARCH = "search";

        #endregion

        #region Fields

        #endregion

        #region Static Props

        public static ApiQuery WildCard
        {

            get
            {
                ApiQuery query = new ApiQuery();
                query.Fields.Add(WILD_CARD_FIELD);

                return query;
            }

        }

        #endregion

        #region Props

        public FieldList Fields { get; } = new FieldList();
        public FieldList ExcludeFields { get; } = new FieldList();

        public string Predicate { get; set; }

        public bool HasPredicate => !string.IsNullOrWhiteSpace(Predicate);

        /// <summary>
        /// The maximum number of results the request should return.
        /// If not set explicitly, the IGDB back-end will assume a default value (usually 10).
        /// </summary>
        public int? Limit { get; set; }
        public int Offset { get; set; }

        public bool HasLimit => Limit.HasValue && Limit.Value >= 0;

        public SortMode SortMode { get; set; } = SortMode.Default;

        public string SearchTerm { get; set; }

        public bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);

        #endregion

        #region Ctors

        public ApiQuery()
        {

        }

        public ApiQuery(IEnumerable<string> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            foreach (string field in fields)
                Fields.Add(field);

        }

        #endregion

        #region Methods

        public override string ToString() => GetQueryString();

        public string GetQueryString()
        {
            const string newLine = "\r\n";

            string queryString = $"fields {Fields};";

            queryString += ExcludeFields.IsEmpty ? string.Empty : $"{newLine}{PARAMETER_NAME_EXCLUDE} {ExcludeFields};";
            queryString += !HasPredicate ? string.Empty : $"{newLine}{PARAMETER_NAME_WHERE} {Predicate};";
            queryString += HasLimit ? $"{newLine}{PARAMETER_NAME_LIMIT} {Limit};" : string.Empty;
            queryString += Offset > 0 ? $"{newLine}{PARAMETER_NAME_OFFSET} {Offset};" : string.Empty;
            queryString += SortMode == SortMode.Default ? string.Empty : $"{newLine}{PARAMETER_NAME_SORT} {(SortMode == SortMode.Ascending ? SORT_MODE_ASCENDING : SORT_MODE_DESCENDING)};";
            queryString += HasSearchTerm ? $"{newLine}{PARAMETER_NAME_SEARCH} \"{SearchTerm}\";" : string.Empty;

            return queryString;
        }

        #endregion

    }
}
