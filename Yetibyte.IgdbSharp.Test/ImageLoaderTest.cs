using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.IgdbSharp.Client;
using Yetibyte.IgdbSharp.Images;

namespace Yetibyte.IgdbSharp.Test
{
    [TestClass]
    public class ImageLoaderTest
    {

        private const string DOWNLOAD_OUTPUT_DIRECTORY = @"C:\Test";

        private const string API_KEY_FILE_PATH = @"C:\Test\igdb_api.txt";

        private string ApiKey => System.IO.File.ReadAllText(API_KEY_FILE_PATH);


        [TestMethod]
        public void DownloadGameEngineLogo()
        {
            const int testGameEngineId = 532;

            IgdbClient client = new IgdbClient(ApiKey);

            var logo = client.GetGameEngines(q => q
                .Fields("logo.*")
                .Where("id = " + testGameEngineId)
            )?.FirstOrDefault()?.Logo;

            byte[] imgData = new ImageLoader().DownloadJpg(logo.ImageId, ImageSize.LogoMed);

            string filePath = DOWNLOAD_OUTPUT_DIRECTORY + "\\" + DateTime.Now.ToString("yyyyMMdd_hh-mm-ss") + ".jpg";

            System.IO.File.WriteAllBytes(filePath, imgData);

        }
    }
}
