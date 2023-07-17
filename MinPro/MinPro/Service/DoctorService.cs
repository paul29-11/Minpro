using MinPro.viewmodels;
using Newtonsoft.Json;

namespace MinPro.Service
{
    public class DoctorService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        //private VMResponse respon = new VMResponse();

        public DoctorService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMDoctor>> GetAllData()
        {
            List<VMDoctor> data = new List<VMDoctor>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiDocter/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMDoctor>>(apiResponse);

            return data;
        }
    }
}
