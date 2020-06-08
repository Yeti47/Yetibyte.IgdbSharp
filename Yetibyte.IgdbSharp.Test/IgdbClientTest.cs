using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yetibyte.IgdbSharp.Client;
using Yetibyte.IgdbSharp.Queries;

namespace Yetibyte.IgdbSharp.Test
{
    [TestClass]
    public class IgdbClientTest
    {
        private const string API_KEY_FILE_PATH = @"C:\Test\igdb_api.txt";

        private string ApiKey => System.IO.File.ReadAllText(API_KEY_FILE_PATH);

        [TestMethod]
        public void GetGameEngines()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            var engines = client.GetGameEngines();

            Assert.IsNotNull(engines);

        }

        [TestMethod]
        public void GetGameEnginesWithCompanies()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            var engines = client.GetGameEngines(q => q
                .Fields("*", "companies.*")
                .Limit(100)
            );

            Assert.IsTrue(engines != null && engines.Any(e => (e.Companies?.Count ?? 0) > 0));

        }

        [TestMethod]
        public void GetGameEnginesWithCompaniesAndLogo()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            var engines = client.GetGameEngines(q => q
                .Fields("*", "companies.*, logo.*")
                .Limit(100)
            );

            Assert.IsTrue(engines != null && engines.Any(e => (e.Companies?.Count ?? 0) > 0));

        }

        [TestMethod]
        public void GetGameEnginesCompanyIds()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            var engines = client.GetGameEngines(q => q
                .Fields("name", "companies")
                .Limit(100)
            );

            Assert.IsTrue(engines != null && engines.Any(e => (e.Companies?.Count ?? 0) > 0));

        }

        [TestMethod]
        public async Task GetGameEnginesAsync()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            var engines = await client.GetGameEnginesAsync();

            Assert.IsNotNull(engines);

        }

        [TestMethod]
        public void GetGameEnginesOnlyNames()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            ApiQuery query = new ApiQuery();
            query.Fields.Add("name");

            var engines = client.GetGameEngines(query);

            Assert.IsNotNull(engines);

        }

        [TestMethod]
        public void GetGameEnginesWildCard()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            ApiQuery query = ApiQuery.WildCard;

            var engines = client.GetGameEngines(query);

            Assert.IsNotNull(engines);

        }

        [TestMethod]
        public void GetGameEnginesWhereIdIn()
        {
            const int someId = 583;
            const int ueId = 203;

            IgdbClient client = new IgdbClient(ApiKey);

            ApiQuery query = ApiQuery.WildCard;
            query.Predicate = $"id = ({someId},{ueId})";

            var engines = client.GetGameEngines(query);

            Assert.IsTrue(engines != null && engines.Count() == 2 && engines.Any(e => e.Id == someId || e.Id == ueId));

        }

        [TestMethod]
        public void GetGameEnginesWithBuilder()
        {
            IgdbClient client = new IgdbClient(ApiKey);

            var engines = client.GetGameEngines(b =>
                b.Fields("name", "companies.*")
                 .Where("id > 500")
                 .Limit(3)
            );

            Assert.IsTrue(engines != null && engines.All(e => e.Id > 500));

        }

        [TestMethod]
        public void GetGameEnginesInvalidPredicate()
        {
            IgdbClient client = new IgdbClient(ApiKey);

            string errorTitle = string.Empty;

            try
            {
                var engines = client.GetGameEngines(b =>
                    b.Fields("name", "companies.*")
                     .Where("id === 500")
                     .Limit(5)
                );
            }
            catch (IgdbClientBadStatusException ex)
            {
                errorTitle = ex?.ErrorInformation?.Title;
            }

            Assert.AreEqual("Syntax Error", errorTitle);

        }

        [TestMethod]
        public void GetGameEnginesInvalidField()
        {

            IgdbClient client = new IgdbClient(ApiKey);

            ApiQuery query = new ApiQuery();
            query.Fields.Add("something");

            string errorTitle = string.Empty;

            try
            {
                var engines = client.GetGameEngines(query);

            }
            catch(IgdbClientBadStatusException ex)
            {
                errorTitle = ex?.ErrorInformation?.Title;
            }

            Assert.AreEqual("Invalid Field", errorTitle);

        }

    }
}
