using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vehiculo.Abstracciones.Modelos;

namespace Web.Pages.Vehiculos
{
    public class DetalleModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VehiculoDetalle Vehiculo { get; set; } = new();
        public string MensajeError { get; set; } = string.Empty;

        public DetalleModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var url = $"{apiBase}/vehiculo/{id}";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                MensajeError = $"URL: {url} | Status: {(int)response.StatusCode} {response.StatusCode} | Body: {body}";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();

            Vehiculo = JsonSerializer.Deserialize<VehiculoDetalle>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();

            return Page();
        }
    }
}