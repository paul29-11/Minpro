using MinPro.datamodels;
using MinPro.viewmodels;
using Newtonsoft.Json;
using System.Text;

namespace MinPro.Service
{
    public class SpesialisasiService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public SpesialisasiService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<MSpecialization>> GetAllData()
        {
            List<MSpecialization> data = new List<MSpecialization>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiSpesialisasi/GetAllData");
            data = JsonConvert.DeserializeObject<List<MSpecialization>>(apiResponse);

            return data;
        }

        public async Task<MSpecialization> GetDataById(int id)
        {
            MSpecialization data = new MSpecialization();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiSpesialisasi/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MSpecialization>(apiResponse);

            return data;
        }

        public async Task<bool> CheckByName(string name, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiSpesialisasi/CheckByName/{name}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<VMResponse> Create(MSpecialization dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PostAsync(RouteAPI + "apiSpesialisasi/Save", content);

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

        public async Task<VMResponse> Edit(MSpecialization dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiSpesialisasi/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //proses membaca respon dari API
                var apiRespon = await request.Content.ReadAsStringAsync();
                //proses convert hasil respon dari API ke object
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;

        }

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiSpesialisasi/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();

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
