using MinPro.viewmodels;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<List<VMTblPasien>> GetDataByIdParent(int id)
        {
            List<VMTblPasien> data = new List<VMTblPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataByIdParent/{id}");
            data = JsonConvert.DeserializeObject<List<VMTblPasien>>(apiResponse);

            return data;
        }

        public async Task<VMTblPasien> GetDataById(int id)
        {
            VMTblPasien data = new VMTblPasien();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMTblPasien>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Create(VMTblPasien dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PostAsync(RouteAPI + "apiPasien/Save", content);

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


        public async Task<VMResponse> Edit(VMTblPasien dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiPasien/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiPasien/Delete/{id}");

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


        public async Task<VMResponse> MultipleDelete(List<int> listId)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(listId);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiPasien/MultipleDelete", content);

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
