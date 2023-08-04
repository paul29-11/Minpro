using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinPro.datamodels;
using MinPro.Models; // Assuming the model for M_BloodGroup is named 'BloodGroup'
using MinPro.viewmodels;
using Newtonsoft.Json;
using MinPro.Services;
using System.Text;

namespace MinPro.Services
{
    public class BloodGroupService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public BloodGroupService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<MBloodGroup>> GetAllData()
        {
            List<MBloodGroup> data = new List<MBloodGroup>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiBloodGroup/GetAllData");
            data = JsonConvert.DeserializeObject<List<MBloodGroup>>(apiResponse);

            return data;
        }


        //private readonly DB_SpesificationContext _context;

        //public BloodGroupService(DB_SpesificationContext context)
        //{
        //    _context = context;
        //}

        //public async Task<List<MBloodGroup>> GetAllBloodGroups()
        //{
        //    return await _context.MBloodGroups.ToListAsync();
        //}

        public async Task<VMResponse> Create(MBloodGroup dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PostAsync(RouteAPI + "apiBloodGroup/Save", content);

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

        public async Task<bool> CheckBloodTypeIsExist(string code, int Id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiBloodGroup/CheckCodeById/{code}/{Id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<MBloodGroup> GetDataById(int id)
        {
            MBloodGroup data = new MBloodGroup();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiBloodGroup/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MBloodGroup>(apiResponse);

            return data;

        }
        public async Task<VMResponse> Edit(MBloodGroup dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiBloodGroup/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiBloodGroup/Delete/{id}");

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
