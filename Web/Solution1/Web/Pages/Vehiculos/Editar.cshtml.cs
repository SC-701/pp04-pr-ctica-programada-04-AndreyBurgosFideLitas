using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vehiculo.Abstracciones.Modelos;

namespace Web.Pages.Vehiculos
{
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public VehiculoRequest Vehiculo { get; set; } = new();

        [BindProperty]
        public Guid IdMarca { get; set; }

        [BindProperty]
        public Guid IdModelo { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public List<SelectListItem> Marcas { get; set; } = new();
        public List<SelectListItem> Modelos { get; set; } = new();

        public EditarModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Id = id;

            await CargarMarcas();

            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{apiBase}/vehiculo/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            var json = await response.Content.ReadAsStringAsync();

            var detalle = JsonSerializer.Deserialize<VehiculoDetalle>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (detalle == null)
                return RedirectToPage("Index");

            Vehiculo = new VehiculoRequest
            {
                IdModelo = Guid.Empty,
                Placa = detalle.Placa,
                Color = detalle.Color,
                Anio = detalle.Anio,
                Precio = detalle.Precio,
                CorreoPropietario = detalle.CorreoPropietario,
                Telefono = detalle.Telefono
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await CargarMarcas();
            await CargarModelos(IdMarca);

            if (!ModelState.IsValid)
                return Page();

            Vehiculo.IdModelo = IdModelo;

            var client = _httpClientFactory.CreateClient();
            var apiBase = _configuration["ApiSettings:BaseUrl"];

            var json = JsonSerializer.Serialize(Vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{apiBase}/vehiculo/{Id}", content);

            if (!response.IsSuccessStatusCode)
                return Page();

            return RedirectToPage("Index");
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
                Text = x.Nombre
            }).ToList();
        }

        private async Task CargarModelos(Guid idMarca)
        {
            if (idMarca == Guid.Empty)
            {
                Modelos = new();
                return;
            }

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