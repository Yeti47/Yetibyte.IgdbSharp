using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yetibyte.IgdbSharp.Queries
{
    public class FieldList : ICollection<string>
    {
        #region Static Fields

        private static readonly char[] INVALID_CHARS = new[] { ' ', ',', '\r' , '\n' };

        #endregion

        #region Fields

        private readonly List<string> _fields = new List<string>();

        #endregion

        #region Props

        public bool IsEmpty => !_fields.Any();

        public int Count => _fields.Count;

        public bool IsReadOnly => false;

        #endregion

        #region Static Methods

        public static bool IsValidFieldName(string fieldName)
        {
            fieldName = fieldName?.Trim();
            return !string.IsNullOrWhiteSpace(fieldName) && !INVALID_CHARS.Any(c => fieldName.Contains(c));

        }

        #endregion

        #region Methods

        public void Add(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item));

            item = item.Trim();

            _fields.Add(item);
        }

        public void Clear() => _fields.Clear();

        public bool Contains(string item) => item != null && _fields.Contains(item.Trim());

        public void CopyTo(string[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            _fields.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator() => _fields.GetEnumerator();

        public bool Remove(string item) => item != null && _fields.Remove(item.Trim());

        IEnumerator IEnumerable.GetEnumerator() => ((ICollection<string>)_fields).GetEnumerator();

        public sealed override string ToString()
        {
            StringBuilder resultBuilder = new StringBuilder();

            foreach (string field in _fields)
            {
                resultBuilder.Append(field);
                resultBuilder.Append(',');
            }

            if (resultBuilder.Length > 0)
                resultBuilder.Length -= 1;

            return resultBuilder.ToString();
        }

        #endregion

    }
}
