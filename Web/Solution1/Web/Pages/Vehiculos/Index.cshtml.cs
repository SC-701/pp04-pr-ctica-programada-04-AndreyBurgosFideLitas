using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vehiculo.Abstracciones.Modelos;

namespace Web.Pages.Vehiculos
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<VehiculoResponse> Vehiculos { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{apiBase}/vehiculo");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            Vehiculos = JsonSerializer.Deserialize<List<VehiculoResponse>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
        }
    }
}