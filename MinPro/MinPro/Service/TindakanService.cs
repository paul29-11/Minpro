using MinPro.datamodels;
using MinPro.viewmodels;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace MinPro.Service
{
    public class TindakanService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public TindakanService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMDoctorTreatment>> GetTreatment(int id)
        {
            List<VMDoctorTreatment> data = new List<VMDoctorTreatment>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiTindakan/GetTreatment/{id}");
            data = JsonConvert.DeserializeObject<List<VMDoctorTreatment>>(apiResponse);

            return data;
        }

        public async Task<bool> CheckByName(string name, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiTindakan/CheckByName/{name}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<TDoctorTreatment> GetDataById(int id)
        {
            TDoctorTreatment data = new TDoctorTreatment();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiTindakan/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<TDoctorTreatment>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Create(TDoctorTreatment dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PostAsync(RouteAPI + "apiTindakan/Save", content);

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

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiTindakan/Delete/{id}");

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
