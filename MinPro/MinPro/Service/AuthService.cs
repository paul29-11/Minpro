using Newtonsoft.Json;

namespace MinPro.Service
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        //private VMResponse respon = new VMResponse();

        public AuthService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        //public async Task<List<VMMenuAccess>> MenuAccess(int IdRole)
        //{
        //    List<VMMenuAccess> data = new List<VMMenuAccess>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/MenuAccess/{IdRole}");
        //    data = JsonConvert.DeserializeObject<List<VMMenuAccess>>(apiResponse)!;

        //    return data;
        //}
    }
}
