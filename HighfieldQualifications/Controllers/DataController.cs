namespace HighfieldQualifications.Controllers
{
    using System.Threading.Tasks;
    using HighfieldQualifications.Contracts;
    using HighfieldQualifications.Services;
    using HighfieldQualifications.ViewModel;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/data")]
    public class DataController : Controller
    {
        private readonly IDataService dataService;

        public DataController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet("calculate")]
        public async Task<ReturnData> Calculate()
        {
            var response = await this.dataService.GetAllData();

            return this.dataService.CalculateReturnData(response);
        }

        [HttpGet("all")]
        public async Task<APIResponse> GetData()
        {
            return await this.dataService.GetAllData();
        }
    }
}
