namespace HighfieldQualifications.Test
{
    using HighfieldQualifications.Contracts;
    using HighfieldQualifications.Services;
    using HighfieldQualifications.Test.objects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class IntegrationTests
    {
        private PersonGenerator personGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.personGenerator = new PersonGenerator();
        }

        [TestMethod]
        [TestCategory(Constants.Category.Integration)]
        public void Check_Calculate_returns_Success()
        {
            var dataService = new DataService();

            var size = 10u;

            var apiResponse = new APIResponse()
            {
                Data = this.personGenerator.CreatePersonsAllProperties(size),
                Details = string.Empty,
                ObjectLayout = string.Empty,
                RequestType = string.Empty,
                Title = string.Empty,
                UriToSubmit = string.Empty
            };

            var results = dataService.CalculateReturnData(apiResponse);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.AgePlusTwenty.Length > 0);
            Assert.IsTrue(results.TopColours.Length > 0);
        }

        [TestMethod]
        [TestCategory(Constants.Category.Integration)]
        public async Task Check_Get_Data_SuccessAsync()
        {
            var dataService = new DataService();

            var results = await dataService.GetAllData();

            Assert.IsNotNull(results);
            Assert.AreEqual(100, results.Data.Count());
        }
    }
}
