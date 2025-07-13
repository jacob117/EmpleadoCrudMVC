using System.Net.Http;
using System.Text;
using System.Text.Json;
using EmpresaFrontend.Models;

public class CatalogoService : ICatalogoService
{  private readonly HttpClient _httpClient;

    public CatalogoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public IEnumerable<Catalogo> ObtenerCatalogo(int id)
    {
      var response = _httpClient.GetAsync($"api/catalogo/{id}").Result;
    response.EnsureSuccessStatusCode();
        var json = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<List<Catalogo>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Catalogo>();
    }
}
