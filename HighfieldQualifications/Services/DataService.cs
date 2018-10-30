namespace HighfieldQualifications.Services
{
    using HighfieldQualifications.Contracts;
    using HighfieldQualifications.ViewModel;
    using log4net;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DataService : IDataService
    {
        private const int AddToAge = 20;
        private const string DataUrl = @"http://recruitment.highfieldqualifications.com/api/gettest";
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataService));

        public ReturnData CalculateReturnData(APIResponse response)
        {
            IEnumerable<Person> people = response.Data;

            Log.Info("Calculating...");

            var result = this.ConvertToCalculatedViewModel(people);

            Log.Info("Successly for calculated the return value data");

            return result;
        }

        public async Task<APIResponse> GetAllData()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(DataUrl);
                var content = response.Content;
                var result = await content.ReadAsStringAsync();

                var externalApiResonse = JsonConvert.DeserializeObject<APIResponse>(result);

                Log.InfoFormat($"Found {externalApiResonse.Data.Count()}");

                if (externalApiResonse == null)
                {
                    Log.WarnFormat("No data found");
                }

                return externalApiResonse;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting data: {0}", ex.Message);

                throw;
            }
        }

        private static int CalculateAge(DateTimeOffset birthDate, DateTimeOffset now)
        {
            var age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }

        private ReturnData ConvertToCalculatedViewModel(IEnumerable<Person> people)
        {
            var ages = new List<int>(people.Count());
            var topColours = new Dictionary<string, int>();

            foreach (var person in people)
            {
                var calculatedAge = CalculateAge(person.Dob, DateTimeOffset.Now);

                ages.Add(calculatedAge + AddToAge);
                
                if (!string.IsNullOrWhiteSpace(person.FavouriteColour)
                    && !topColours.ContainsKey(person.FavouriteColour))
                {
                    topColours.Add(person.FavouriteColour, 1);
                }
                else
                {
                    var value = topColours[person.FavouriteColour];
                    value++;
                    topColours[person.FavouriteColour] = value;
                }
            }

            var topColoursArr = topColours
                .Select(x => new TopColour(){ Colour = x.Key, Amount = x.Value })
                .ToArray();

            return new ReturnData()
            {
                AgePlusTwenty = ages.ToArray(),
                TopColours = topColoursArr
            };
        }
    }
}
