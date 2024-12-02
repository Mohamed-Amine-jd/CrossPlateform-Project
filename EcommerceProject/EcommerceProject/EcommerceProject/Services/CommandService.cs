using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace EcommerceProject.Services
{
    public class CommandService
    {
        private readonly HttpClient _httpClient;

        public CommandService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://172.20.10.4:8080/api/command/") }; // Update with your backend URL
        }

        // Send command to the backend (for checkout)
        public async Task<bool> CreateCommand(Command command)
        {
            var json = JsonConvert.SerializeObject(command);  // Serialize the command object
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("create", content); // Send to backend

            return response.IsSuccessStatusCode;
        }

        // Retrieve all commands from the backend
        public async Task<List<Command>> GetAllCommands()
        {
            var response = await _httpClient.GetAsync("all"); // Assuming 'all' endpoint exists on your backend

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var commands = JsonConvert.DeserializeObject<List<Command>>(json);
                return commands;
            }

            return new List<Command>();
        }
    }
}
