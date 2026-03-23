using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vehiculo.Abstracciones.Modelos;

namespace Web.Pages.Vehiculos
{
    public class EliminarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VehiculoDetalle Vehiculo { get; set; } = new();

        [BindProperty]
        public Guid Id { get; set; }

        public EliminarModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Id = id;

            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{apiBase}/vehiculo/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            var json = await response.Content.ReadAsStringAsync();

            Vehiculo = JsonSerializer.Deserialize<VehiculoDetalle>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.DeleteAsync($"{apiBase}/vehiculo/{Id}");

            if (!response.IsSuccessStatusCode)
                return Page();

            return RedirectToPage("Index");
        }
    }
}