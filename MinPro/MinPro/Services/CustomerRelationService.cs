using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinPro.datamodels;
using MinPro.Models; // Assuming the model for M_Customer_Relation is named 'CustomerRelation'
using MinPro.viewmodels;
using Newtonsoft.Json;

namespace MinPro.Services
{
    public class CustomerRelationService
    {
            private static readonly HttpClient client = new HttpClient();
            private IConfiguration configuration;
            private string RouteAPI = "";
            private VMResponse respon = new VMResponse();

            public CustomerRelationService(IConfiguration _configuration)
            {
                this.configuration = _configuration;
                this.RouteAPI = this.configuration["RouteAPI"];
            }

            public async Task<List<MCustomerRelation>> GetAllData()
            {
                List<MCustomerRelation> data = new List<MCustomerRelation>();

                string apiResponse = await client.GetStringAsync(RouteAPI + "apiCustomerRelation/GetAllData");
                data = JsonConvert.DeserializeObject<List<MCustomerRelation>>(apiResponse);

                return data;
            }
        //    private readonly DB_SpesificationContext _context;

        //public CustomerRelationService(DB_SpesificationContext context)
        //{
        //    _context = context;
        //}

        //public async Task<List<MCustomerRelation>> GetAllCustomerRelations()
        //{
        //    return await _context.MCustomerRelations.ToListAsync();
        //}

        public async Task<VMResponse> Create(MCustomerRelation dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PostAsync(RouteAPI + "apiCustomerRelation/Save", content);

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

        public async Task<bool> CheckNameById(string name, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomerRelation/CheckNameById/{name}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<MCustomerRelation> GetDataById(int id)
        {
            MCustomerRelation data = new MCustomerRelation();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomerRelation/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MCustomerRelation>(apiResponse);

            return data;

        }
        public async Task<VMResponse> Edit(MCustomerRelation dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiCustomerRelation/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiCustomerRelation/Delete/{id}");

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
