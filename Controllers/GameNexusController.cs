using Game_Nexus.Models; // <-- Actualizado al nuevo namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Game_Nexus.Controllers // <-- Actualizado al nuevo namespace
{
    public class CatalogoController : Controller
    {
        // Lista estática con las URLs DIRECTAS corregidas
        private static List<Item> _items = new()
        {
            new Item {
                Id = 1, Titulo = "Devil May Cry", Genero = "Hack and Slash", Ano = 2001,
                Plataforma = "PlayStation 2", // <-- Antes "Consola"
                MotorGrafico = "MT Framework", // <-- Nuevo campo de la arquitectura
                EstadoProgreso = "Completado", // <-- Nuevo campo de la arquitectura
                Descripcion = "Videojuego que trata de un cazador de demonios",
                // URL corregida a la portada del primer juego
                ImagenUrl = "https://images.hdqwalls.com/download/dante-in-devil-may-cry-5-4k-0r-2560x1440.jpg"
            },
            new Item {
                Id = 2, Titulo = "Castlevania Simphony of the night", Genero = "Metroidvania", Ano = 1997,
                Plataforma = "PlayStation",
                MotorGrafico = "Custom 2D",
                EstadoProgreso = "Completado",
                Descripcion = "Videojuego que trata de un cazador de vampiros en el castillo de Drácula",
                // URL corregida
                ImagenUrl = "https://tse2.mm.bing.net/th/id/OIP.Y4SAVYOvyo3dtymin9-_awHaEK?rs=1&pid=ImgDetMain&o=7&rm=3"
            },
            new Item {
                Id = 3, Titulo = "Resident Evil 4", Genero = "Survival Horror", Ano = 2005,
                Plataforma = "GameCube",
                MotorGrafico = "Custom RE Engine",
                EstadoProgreso = "Completado",
                Descripcion = "Videojuego que trata de un agente especial en una misión en España",
                // URL corregida
                ImagenUrl = "https://tse4.mm.bing.net/th/id/OIP.O4n-qkNaO5nWO0L6OKR2dgHaEK?rs=1&pid=ImgDetMain&o=7&rm=3"
            },
            new Item {
                Id = 4, Titulo = "Halo", Genero = "Ciencia ficcion", Ano = 2001,
                Plataforma = "Xbox",
                MotorGrafico = "Blam! Engine",
                EstadoProgreso = "Completado",
                Descripcion = "Videojuego que trata de un espartano que pelea contra el Covenant",
                // URL corregida
                ImagenUrl = "https://tse4.mm.bing.net/th/id/OIP.tFBVKpjMidVymPgbDkeo0wHaEK?rs=1&pid=ImgDetMain&o=7&rm=3"
            },
            new Item {
                Id = 5, Titulo = "God of War", Genero ="Aventura", Ano = 2005,
                Plataforma = "PlayStation 2",
                MotorGrafico = "Kinetica",
                EstadoProgreso = "Completado",
                Descripcion = "Videojuego que trata de un semidios que pelea con dioses griegos",
                // URL corregida
                ImagenUrl = "https://tse2.mm.bing.net/th/id/OIP.dmidYTqYnMY314BxxS0k4wHaEK?rs=1&pid=ImgDetMain&o=7&rm=3"
            }
        };

        // ========================================================
        // LOS MÉTODOS DE ABAJO QUEDARON INTACTOS (SIN CAMBIAR NADA)
        // ========================================================

        public IActionResult Index(string? genero)
        {
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero == genero).ToList();

            ViewBag.Generos = _items.Select(i => i.Genero).Distinct().ToList();
            ViewBag.GeneroActual = genero;

            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            item.Id = _items.Count > 0 ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
            return RedirectToAction("Index");
        }
    }
}