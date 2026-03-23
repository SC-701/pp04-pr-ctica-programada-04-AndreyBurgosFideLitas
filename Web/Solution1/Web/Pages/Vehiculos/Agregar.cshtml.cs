using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vehiculo.Abstracciones.Modelos;

namespace Web.Pages.Vehiculos
{
    public class AgregarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public VehiculoRequest Vehiculo { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid IdMarca { get; set; }

        [BindProperty]
        public Guid IdModelo { get; set; }

        public List<SelectListItem> Marcas { get; set; } = new();
        public List<SelectListItem> Modelos { get; set; } = new();

        public AgregarModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            await CargarMarcas();

            if (IdMarca != Guid.Empty)
                await CargarModelos(IdMarca);
        }

        public async Task<IActionResult> OnPost()
        {
            await CargarMarcas();

            if (IdMarca != Guid.Empty)
                await CargarModelos(IdMarca);

            if (!ModelState.IsValid)
                return Page();

            Vehiculo.IdModelo = IdModelo;

            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var json = JsonSerializer.Serialize(Vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiBase}/vehiculo", content);

            if (!response.IsSuccessStatusCode)
                return Page();

            return RedirectToPage("/Vehiculos/Index");
        }

        private async Task CargarMarcas()
        {
            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{apiBase}/marca");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var marcas = JsonSerializer.Deserialize<List<Marca>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            Marcas = marcas.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombre,
                Selected = x.Id == IdMarca
            }).ToList();
        }

        private async Task CargarModelos(Guid idMarca)
        {
            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{apiBase}/modelo/{idMarca}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var modelos = JsonSerializer.Deserialize<List<Modelo>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            Modelos = modelos.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombre
            }).ToList();
        }
    }
}