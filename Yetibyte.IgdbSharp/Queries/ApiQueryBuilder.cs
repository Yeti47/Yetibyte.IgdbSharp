using System;
using System.IO;
using System.Text;

namespace Yetibyte.IgdbSharp.Queries
{
    public class ApiQueryBuilder
    {
        #region Fields

        private readonly FieldList _fields = new FieldList();
        private readonly FieldList _excludeFields = new FieldList();

        private string _predicate;
        private int? _limit;
        private int _offset;
        private SortMode _sortMode;
        private string _searchTerm;

        #endregion

        #region Methods

        public ApiQueryBuilder Fields(params string[] fieldNames)
        {
            if (fieldNames is null)
            {
                throw new ArgumentNullException(nameof(fieldNames));
            }

            _fields.Clear();

            foreach (string field in fieldNames)
                _fields.Add(field);

            return this;

        }

        public ApiQueryBuilder Exclude(params string[] fieldNames)
        {
            if (fieldNames is null)
            {
                throw new ArgumentNullException(nameof(fieldNames));
            }

            _excludeFields.Clear();

            foreach (string field in fieldNames)
                _excludeFields.Add(field);

            return this;

        }

        public ApiQueryBuilder Where(string predicate)
        {

            _predicate = predicate;
            return this;

        }

        public ApiQueryBuilder Limit(int? limit)
        {

            _limit = limit;
            return this;

        }

        public ApiQueryBuilder Offset(int offset)
        {

            _offset = offset;
            return this;

        }

        public ApiQueryBuilder SortMode(SortMode sortMode)
        {

            _sortMode = sortMode;
            return this;

        }

        public ApiQueryBuilder Search(string term)
        {
            _searchTerm = term;
            return this;
        }

        public ApiQuery Build()
        {
            ApiQuery apiQuery = new ApiQuery(_fields)
            {
                Limit = _limit,
                Offset = _offset,
                Predicate = _predicate,
                SearchTerm = _searchTerm,
                SortMode = _sortMode
            };

            foreach (string field in _excludeFields)
                apiQuery.ExcludeFields.Add(field);

            return apiQuery;

        }

        #endregion

    }
}
