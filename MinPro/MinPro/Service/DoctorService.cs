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

        public async Task<VMDoctor> GetById(int id)
        {
            VMDoctor data = new VMDoctor();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiDocter/GetById/{id}");
            data = JsonConvert.DeserializeObject<VMDoctor>(apiResponse);

            return data;
        }
        //public async Task<VMDoctor> GetHarga(int id)
        //{
        //    VMDoctor data = new VMDoctor();
        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiDocter/GetHarga/{id}");
        //    data = JsonConvert.DeserializeObject<VMDoctor>(apiResponse);

        //    return data;
        //}
        //public async Task<List<VMDoctor>> GetPendidikan()
        //{
        //    List<VMDoctor> data = new List<VMDoctor>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + "apiDocter/GetPendidikan");
        //    data = JsonConvert.DeserializeObject<List<VMDoctor>>(apiResponse);

        //    return data;
        //}

        //public async Task<List<VMDoctor>> GetTindakanMedis()
        //{
        //    List<VMDoctor> data = new List<VMDoctor>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + "apiDocter/GetTindakanMedis");
        //    data = JsonConvert.DeserializeObject<List<VMDoctor>>(apiResponse);

        //    return data;
        //}

        //public async Task<List<VMDoctor>> GetRiwayatPraktek()
        //{
        //    List<VMDoctor> data = new List<VMDoctor>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + "apiDocter/GetRiwayatPraktek");
        //    data = JsonConvert.DeserializeObject<List<VMDoctor>>(apiResponse);

        //    return data;
        //}


    }
}
