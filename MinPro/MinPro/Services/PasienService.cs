using MinPro.viewmodels;
using Newtonsoft.Json;

namespace MinPro.Services
{
    public class PasienService
    {

        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();
        public PasienService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMTblPasien>> GetAllData()
        {
            List<VMTblPasien> data = new List<VMTblPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiPasien/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMTblPasien>>(apiResponse);

            // Add debug log to check "data"
            Console.WriteLine(JsonConvert.SerializeObject(data));
            return data;
        }

        //public async Task<VMTblPasien> GetDataById(int id)
        //{
        //    VMTblPasien data = new VMTblPasien();
        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
        //    data = JsonConvert.DeserializeObject<VMTblPasien>(apiResponse);

        //    return data;

        //}

        public async Task<List<VMTblPasien>> GetDataById(int id)
        {
            List<VMTblPasien> data = new List<VMTblPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<List<VMTblPasien>>(apiResponse);

            return data;

        }
    }
}
