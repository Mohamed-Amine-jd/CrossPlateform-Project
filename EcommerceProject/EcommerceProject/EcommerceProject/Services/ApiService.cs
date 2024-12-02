using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EcommerceProject
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://172.20.10.4:8080/api/auth/") };
        }

        public async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            var user = new
            {
                name = name,
                email = email,
                password = password,
                phone = phone
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool success, bool isAdmin, string userId)> LoginUser(string email, string password)
        {
            var user = new
            {
                email = email,
                password = password
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                return (true, result.IsAdmin, result.userId);
            }

            return (false, false, null);
        }

        public class LoginResponse
        {
            public bool IsAdmin { get; set; }
            public string userId { get; set; }





        }
    }
}
