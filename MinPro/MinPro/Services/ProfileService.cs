using MinPro.viewmodels;
using Newtonsoft.Json;
using System.Text;
using xpos319.viewmodels;

namespace MinPro.Services
{
    public class ProfileService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public ProfileService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }
        public async Task<List<VMTblProfile>> GetAllData()
        {
            List<VMTblProfile> data = new List<VMTblProfile>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiProfile/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMTblProfile>>(apiResponse);

            return data;
        }

        public async Task<VMTblProfile> GetDataById(int id)
        {
            VMTblProfile data = new VMTblProfile();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMTblProfile>(apiResponse);

            return data;

        }

        public async Task<VMResponse> Edit(VMTblProfile dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                // Proses membaca respon membca api
                var apiRespon = await request.Content.ReadAsStringAsync();

                // Proses Convert hasil respon dari api ke objeck
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";

            }

            return respon;
        }
        public async Task<VMResponse> EditM(VMTblProfile dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/EditM", content);

            if (request.IsSuccessStatusCode)
            {
                // Proses membaca respon membca api
                var apiRespon = await request.Content.ReadAsStringAsync();

                // Proses Convert hasil respon dari api ke objeck
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";

            }

            return respon;
        }
        public async Task<VMResponse> EditP(VMTblProfile dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/EditP", content);

            if (request.IsSuccessStatusCode)
            {
                // Proses membaca respon membca api
                var apiRespon = await request.Content.ReadAsStringAsync();

                // Proses Convert hasil respon dari api ke objeck
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";

            }

            return respon;
        }

    }

}
