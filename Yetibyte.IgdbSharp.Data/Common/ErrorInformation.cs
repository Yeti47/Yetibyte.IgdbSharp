using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.IgdbSharp.Data.Common
{
    /// <summary>
    /// Describes a server-side error.
    /// </summary>
    public class ErrorInformation
    {
        /// <summary>
        /// Short title describing the error.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The http error status code.
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// A short text describing what caused the error.
        /// </summary>
        public string Cause { get; private set; }

        /// <summary>
        /// If available, provides more detailed information about the error that occurred.
        /// </summary>
        public string Details { get; private set; }

        public string Type { get; set; }

        /// <summary>
        /// Default parameterless constructor for deserialization purposes.
        /// </summary>
        public ErrorInformation()
        {

        }

        /// <summary>
        /// Constructs a new instance of <see cref="ErrorInformation"/>.
        /// </summary>
        /// <param name="title">Short title describing the error.</param>
        /// <param name="status">The http error status code.</param>
        /// <param name="cause">A short text describing what caused the error.</param>
        /// <param name="details">If available, provides more detailed information about the error that occurred.</param>
        /// <param name="type"></param>
        public ErrorInformation(string title, int status, string cause, string details, string type = null)
        {
            Title = title;
            Status = status;
            Cause = cause;
            Details = details;
            Type = type;
        }

    }
}
