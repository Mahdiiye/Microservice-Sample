using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to CommandServoce Was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync Post to CommandServoce Was Not OK!");
            }
        }
    }
}
