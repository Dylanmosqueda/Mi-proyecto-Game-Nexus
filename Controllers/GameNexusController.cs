using Game_Nexus.Models; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_Nexus.Controllers 
{
    public class GameNexusController : Controller
    {
        // Lista estática actualizada con los nuevos campos de Halo 4 / UNSC Archive
        private static List<Item> _items = new()
        {
            new Item {
                Id = 1, Titulo = "Devil May Cry", Genero = "Hack and Slash", Ano = 2001,
                Consola = "PlayStation 2", Descripcion = "Videojuego que trata de un cazador de demonios",
                ImagenUrl = "https://images.hdqwalls.com/download/dante-in-devil-may-cry-5-4k-0r-2560x1440.jpg",
                // Nuevos Campos
                Desarrollador = "CapCom", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2021, 05, 10), HorasDedicadas = 45,
                Calificacion = 9, VinculacionProyecto = "Referencia de combos y combate fluido"
            },
            new Item {
                Id = 2, Titulo = "Castlevania Simphony of the night", Genero = "Metroidvania", Ano = 1997,
                Consola = "PlayStation", Descripcion = "Videojuego que trata de un cazador de vampiros en el castillo de Drácula",
                ImagenUrl = "https://tse2.mm.bing.net/th/id/OIP.Y4SAVYOvyo3dtymin9-_awHaEK?rs=1&pid=ImgDetMain&o=7&rm=3",
                // Nuevos Campos
                Desarrollador = "Konami", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2020, 11, 15), HorasDedicadas = 60,
                Calificacion = 10, VinculacionProyecto = "Estudio de backtracking y diseño de niveles"
            },
            new Item {
                Id = 3, Titulo = "Resident Evil 4", Genero = "Survival Horror", Ano = 2005,
                Consola = "GameCube", Descripcion = "Videojuego que trata de un agente especial en una misión en España",
                ImagenUrl = "https://tse4.mm.bing.net/th/id/OIP.O4n-qkNaO5nWO0L6OKR2dgHaEK?rs=1&pid=ImgDetMain&o=7&rm=3",
                // Nuevos Campos
                Desarrollador = "CapCom", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2022, 03, 20), HorasDedicadas = 85,
                Calificacion = 10, VinculacionProyecto = "Análisis de cámara al hombro e IA enemiga"
            },
            new Item {
                Id = 4, Titulo = "Halo", Genero = "Ciencia ficcion", Ano = 2001,
                Consola = "Xbox", Descripcion = "Videojuego que trata de un espartano que pelea contra el Covenant",
                ImagenUrl = "https://tse4.mm.bing.net/th/id/OIP.tFBVKpjMidVymPgbDkeo0wHaEK?rs=1&pid=ImgDetMain&o=7&rm=3",
                // Nuevos Campos
                Desarrollador = "343 Industries", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2023, 01, 05), HorasDedicadas = 120,
                Calificacion = 9, VinculacionProyecto = "Inspiración para estética HUD y Sci-Fi"
            },
            new Item {
                Id = 5, Titulo = "God of War", Genero ="Aventura", Ano = 2005,
                Consola = "PlayStation 2", Descripcion = "Videojuego que trata de un semidios que pelea con dioses griegos",
                ImagenUrl = "https://tse2.mm.bing.net/th/id/OIP.dmidYTqYnMY314BxxS0k4wHaEK?rs=1&pid=ImgDetMain&o=7&rm=3",
                // Nuevos Campos
                Desarrollador = "SIE Santa Monica Studio", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2019, 08, 12), HorasDedicadas = 50,
                Calificacion = 9, VinculacionProyecto = "Estudio de cinematicas in-game y Quick Time Events"
            },
            new Item {
                Id = 6, Titulo = "Call of Duty", Genero ="Shooter", Ano = 2003,
                Consola = "Xbox", Descripcion = "Videojuego que trata de batallas de guerra",
                ImagenUrl = "https://gmedia.playstation.com/is/image/SIEPDC/call-of-duty-franchise-hub-keyart-01-en-21nov23?$facebook$",
                // Nuevos Campos
                Desarrollador = "Infinity Ward", EstadoProgreso = "Completado",
                FechaAdquisicion = new DateTime(2025, 05, 24), HorasDedicadas = 50,
                Calificacion = 10, VinculacionProyecto = " Es en primera persona de estilo belico"
            }
        };

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