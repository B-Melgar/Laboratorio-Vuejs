using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class ListaCompraController : Controller
{
    
    private List<ListaCompra> listaCompras = new List<ListaCompra>();
    private int nextId = 1;

    // Acción para mostrar la lista de compras
    public IActionResult Index()
    {
        var httpClient = new HttpClient();
        var response = httpClient.GetAsync("https://tuservicioweb.com/api/listacompras").Result;
        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsAsync<List<ListaCompra>>().Result;
            return View(data);
        }
        return View(new List<ListaCompra>());
    }

    // Acción para crear una nueva compra (mostrar la vista de creación)
    public IActionResult Crear()
    {
        return View();
    }

    // Acción para procesar la creación de una nueva compra
    [HttpPost]
    public IActionResult Crear(ListaCompra nuevaCompra)
    {
        nuevaCompra.Id = nextId++;
        listaCompras.Add(nuevaCompra);
        return RedirectToAction("Index");
    }

    // Acción para editar una compra existente (mostrar la vista de edición)
    public IActionResult Editar(int id)
    {
        var compra = listaCompras.Find(c => c.Id == id);
        if (compra == null)
        {
            return NotFound(); // Manejo de error si la compra no se encuentra
        }
        return View(compra);
    }

    // Acción para procesar la edición de una compra
    [HttpPost]
    public IActionResult Editar(ListaCompra compraActualizada)
    {
        var compra = listaCompras.Find(c => c.Id == compraActualizada.Id);
        if (compra == null)
        {
            return NotFound(); // Manejo de error si la compra no se encuentra
        }

        compra.Nombre = compraActualizada.Nombre;
        compra.Cantidad = compraActualizada.Cantidad;
        compra.Realizado = compraActualizada.Realizado;

        return RedirectToAction("Index");
    }

    // Acción para eliminar una compra
    public IActionResult Eliminar(int id)
    {
        var compra = listaCompras.Find(c => c.Id == id);
        if (compra == null)
        {
            return NotFound(); // Manejo de error si la compra no se encuentra
        }

        listaCompras.Remove(compra);
        return RedirectToAction("Index");
    }
}
