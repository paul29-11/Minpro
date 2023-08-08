using MinPro.viewmodels;
using Newtonsoft.Json;
using System.Text;
using MinPro.viewmodels;
using MinPro.datamodels;

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
        public async Task<MUser> GetDataByIdUser(int id)
        {
            MUser data = new MUser();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/GetDataByIdUser/{id}");
            data = JsonConvert.DeserializeObject<MUser>(apiResponse);

            return data;

        }

        public async Task<bool> CheckByPassword(string password, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/CheckByPassword/{password}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }
        public async Task<bool> CheckByEmail(string email, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/CheckByEmail/{email}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<VMResponse> CheckOTP(string token, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/CheckOTP/{token}/{id}");
            VMResponse respon = JsonConvert.DeserializeObject<VMResponse>(apiResponse);

            return respon;
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
        
        public async Task<VMResponse> SureEditP(MUser dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/SureEditP", content);

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

        public async Task<VMResponse> SendOTP(MUser dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/SendOTP", content);

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

        public async Task<VMResponse> ResendOTP(MUser dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/ResendOTP", content);

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

        public async Task<VMResponse> SubmitMail(MUser dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/SubmitMail", content);

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


        //public async Task<VMResponse> CheckOTP(string token, int id)
        //{
        //    // Proses convert dari objek ke string
        //    string json = JsonConvert.SerializeObject(dataParam);

        //    // Preses convert string menjadi Json lalu dikirim sbagai request body
        //    StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        //    // Proses memanggil api dan mengirimkan body
        //    var request = await client.PutAsync(RouteAPI + $"apiProfile/CheckOTP/{token}/{id}");

        //    if (request.IsSuccessStatusCode)
        //    {
        //        // Proses membaca respon membca api
        //        var apiRespon = await request.Content.ReadAsStringAsync();

        //        // Proses Convert hasil respon dari api ke objeck
        //        respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
        //    }
        //    else
        //    {
        //        respon.Success = false;
        //        respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";

        //    }

        //    return respon;
    }

}

