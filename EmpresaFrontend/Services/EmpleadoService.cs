using System.Net.Http;
using System.Text;
using System.Text.Json;
using EmpresaFrontend.Models;

public class EmpleadoService : IEmpleadoService
{
    private readonly HttpClient _httpClient;

    public EmpleadoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IEnumerable<Empleado> ObtenerTodos()
    {
        var response = _httpClient.GetAsync("api/empleados").Result;
        response.EnsureSuccessStatusCode();
        var json = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<List<Empleado>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Empleado>();
    }

 public Empleado ObtenerPorId(int id)
{
    var response = _httpClient.GetAsync($"api/empleados/{id}").Result;
    response.EnsureSuccessStatusCode();
    var json = response.Content.ReadAsStringAsync().Result;

    Console.WriteLine("Respuesta de la API:");
    Console.WriteLine(json);

    return JsonSerializer.Deserialize<Empleado>(json, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;
}


    public void Crear(Empleado emp)
    {
        var json = JsonSerializer.Serialize(emp);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync("api/empleados", content).Result;
        response.EnsureSuccessStatusCode();
    }

    public void Actualizar(Empleado emp)
    {
        var json = JsonSerializer.Serialize(emp);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PutAsync($"api/empleados/{emp.Codigo}", content).Result;
        response.EnsureSuccessStatusCode();
        Console.WriteLine("Actualizar json");
        Console.WriteLine(json);

    }

    public void Eliminar(int id)
    {
        var response = _httpClient.DeleteAsync($"api/empleados/{id}").Result;
        response.EnsureSuccessStatusCode();
    }
}
