using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Yetibyte.IgdbSharp.Client;
using Yetibyte.IgdbSharp.Data.Images;

namespace Yetibyte.IgdbSharp.Images
{
    public class ImageLoader
    {

        #region Constants

        private const string IMAGE_URL_TEMPLATE = "https://images.igdb.com/igdb/image/upload/t_{size}/{hash}.jpg";

        private const string URL_PLACEHOLDER_SIZE = "{size}";
        private const string URL_PLACEHOLDER_HASH = "{hash}";

        private const string DOUBLE_SIZE_SUFFIX = "_2x";

        private const string ERR_MSG_MISSING_IMAGE_ID = "An image ID must be provided.";

        #endregion

        #region Fields

        private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

        #endregion

        #region Props

        public bool EnableCaching { get; set; }

        #endregion

        #region Ctors

        public ImageLoader(bool enableCaching = false)
        {
            EnableCaching = enableCaching;
        }

        #endregion

        #region Methods

        public string BuildImageUrl(string imageId, ImageSize imageSize, bool doubleSize = false)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentException(ERR_MSG_MISSING_IMAGE_ID, nameof(imageId));
            }

            if (imageSize is null)
            {
                throw new ArgumentNullException(nameof(imageSize));
            }

            return IMAGE_URL_TEMPLATE
                .Replace(URL_PLACEHOLDER_SIZE, URL_PLACEHOLDER_SIZE + (doubleSize ? DOUBLE_SIZE_SUFFIX : string.Empty))
                .Replace(URL_PLACEHOLDER_SIZE, imageSize.Name)
                .Replace(URL_PLACEHOLDER_HASH, imageId);

        }

        public byte[] DownloadJpg(string imageId, ImageSize imageSize, bool doubleSize = false)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentException(ERR_MSG_MISSING_IMAGE_ID, nameof(imageId));
            }

            if (imageSize is null)
            {
                throw new ArgumentNullException(nameof(imageSize));
            }

            byte[] data = null;

            if(EnableCaching && _cache.TryGetValue(imageId, out data))
            {

                return data;

            }

            using (WebClient webClient = new WebClient())
            {

                string url = BuildImageUrl(imageId, imageSize, doubleSize);
                data = webClient.DownloadData(url);

                if (EnableCaching)
                    _cache.Add(imageId, data);

                return data;

            } 

        }

        #endregion

    }
}
