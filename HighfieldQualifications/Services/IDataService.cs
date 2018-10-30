namespace HighfieldQualifications.Services
{
    using HighfieldQualifications.Contracts;
    using HighfieldQualifications.ViewModel;
    using System.Threading.Tasks;

    public interface IDataService
    {
        Task<APIResponse> GetAllData();
        ReturnData CalculateReturnData(APIResponse response);
    }
}
