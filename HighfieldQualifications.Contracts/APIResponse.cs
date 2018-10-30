namespace HighfieldQualifications.Contracts
{
    using System.Collections.Generic;

    public class APIResponse
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string RequestType { get; set; }
        public string UriToSubmit { get; set; }
        public string ObjectLayout { get; set; }
        public IEnumerable<Person> Data { get; set; }
    }
}