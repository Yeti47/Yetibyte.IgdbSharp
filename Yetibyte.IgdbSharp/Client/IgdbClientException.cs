using System;

namespace Yetibyte.IgdbSharp.Client
{
    /// <summary>
    /// An exception that indicates that an error occurred while executing an operation of an <see cref="IgdbClient"/>.
    /// </summary>
    public class IgdbClientException : Exception
    {
        public IgdbClientException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public IgdbClientException(string message) :  base(message)
        {

        }
    }
}
