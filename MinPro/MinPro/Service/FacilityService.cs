using AutoMapper;
using MinPro.datamodels;
using MinPro.viewmodels;
using Newtonsoft.Json;

namespace MinPro.Service
{
    public class FacilityService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        //private VMResponse respon = new VMResponse();

        public FacilityService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMMedicalFacility>> GetAllData()
        {
            List<VMMedicalFacility> data = new List<VMMedicalFacility>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiFacility/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMMedicalFacility>>(apiResponse);

            return data;
        }


    }
}
