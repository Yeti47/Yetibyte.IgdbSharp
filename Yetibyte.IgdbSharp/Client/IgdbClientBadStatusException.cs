using System.Net;
using Yetibyte.IgdbSharp.Data.Common;

namespace Yetibyte.IgdbSharp.Client
{
    public class IgdbClientBadStatusException : IgdbClientException
    {

        private readonly string _customMessage;

        public ErrorInformation ErrorInformation { get; private set; }

        public override string Message => string.IsNullOrWhiteSpace(_customMessage) ? ErrorInformation?.Cause : _customMessage;

        public IgdbClientBadStatusException(ErrorInformation errorInfo) : base(string.Empty)
        {
            ErrorInformation = errorInfo;
        }

        public IgdbClientBadStatusException(string message) : this((ErrorInformation)null)
        {
            _customMessage = message;
        }

    }

}
