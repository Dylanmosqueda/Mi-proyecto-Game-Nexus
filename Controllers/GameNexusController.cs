using Game_Nexus.Models;
using Microsoft.AspNetCore.Mvc;

namespace Game_Nexus.Controllers
{
    public class GameNexusController : Controller
    {
        private static List<Item> _items = new()
        {
            new Item { Id = 1, Titulo = "Devil May Cry", Genero = "Hack and Slash", Ano = 2001, Consola = "PlayStation 2",
                       Descripcion = "Cazador de demonios", Desarrollador = "CapCom", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2021, 5, 10), HorasDedicadas = 45, Calificacion = 9, ImagenUrl = "https://images.hdqwalls.com/wallpapers/devil-may-cry-5-game-ld.jpg" },

            new Item { Id = 2, Titulo = "Castlevania Symphony of the Night", Genero = "Metroidvania", Ano = 1997, Consola = "PlayStation",
                       Descripcion = "Cazador de vampiros", Desarrollador = "Konami", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2020, 11, 15), HorasDedicadas = 60, Calificacion = 10, ImagenUrl = "https://th.bing.com/th/id/R.41321258bcc7fd4cb3572b727b198bc7?rik=bQPBmF4H1%2bR8VA&riu=http%3a%2f%2fwww.konami.com%2fgames%2fcastlevania%2fs%2fimg%2fsns_castlevania_us.jpg&ehk=z2jCWSz1QWLqNUNk6cvkAkgOFheD35uKOa3XDv4P4us%3d&risl=&pid=ImgRaw&r=0" },

            new Item { Id = 3, Titulo = "Resident Evil 4", Genero = "Survival Horror", Ano = 2005, Consola = "GameCube",
                       Descripcion = "Agente especial en España", Desarrollador = "CapCom", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2022, 3, 20), HorasDedicadas = 85, Calificacion = 10, ImagenUrl = "https://4kwallpapers.com/images/wallpapers/resident-evil-4-3440x1440-11094.jpg" },

            new Item { Id = 4, Titulo = "Halo", Genero = "Ciencia ficcion", Ano = 2001, Consola = "Xbox",
                       Descripcion = "Espartano contra el Covenant", Desarrollador = "343 Industries", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2023, 1, 5), HorasDedicadas = 120, Calificacion = 9, ImagenUrl = "https://th.bing.com/th/id/R.16e1c5ae8c63b39f05f2411f10e09412?rik=Qaxz3pjdxRzghA&pid=ImgRaw&r=0" },

            new Item { Id = 5, Titulo = "God of War", Genero ="Aventura", Ano = 2005, Consola = "PlayStation 2",
                       Descripcion = "Semidios contra dioses griegos", Desarrollador = "SIE Santa Monica Studio", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2019, 8, 12), HorasDedicadas = 50, Calificacion = 9, ImagenUrl = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2025/02/god-of-war.jpg"},

            new Item { Id = 6, Titulo = "Call of Duty", Genero ="Shooter", Ano = 2003, Consola = "Xbox",
                       Descripcion = "Batallas de guerra", Desarrollador = "Infinity Ward", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2025, 5, 24), HorasDedicadas = 50, Calificacion = 10, ImagenUrl = "https://staticg.sportskeeda.com/editor/2023/10/c3dec-16987519192024-1920.jpg" },
            new Item{ Id = 7, Titulo = "The Legend of Zelda: Breath of the Wild", Genero = "Aventura", Ano = 2017, Consola = "Nintendo Switch",
                       Descripcion = "Exploración en mundo abierto", Desarrollador = "Nintendo", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2020, 2, 14), HorasDedicadas = 150, Calificacion = 10, ImagenUrl = "https://th.bing.com/th/id/R.cba83cbe522f092fc0b57be7224dbe11?rik=32Pd%2bGVHcoz8LA&pid=ImgRaw&r=0" },
            new Item{ Id = 8, Titulo = "Red Dead Redemption 2", Genero = "Aventura", Ano = 2018, Consola = "PlayStation 4",
                       Descripcion = "Vida de forajido en el viejo oeste", Desarrollador = "Rockstar Games", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2021, 6, 30), HorasDedicadas = 200, Calificacion = 10, ImagenUrl = "https://th.bing.com/th/id/R.32faa1e71871f9d5905830ec57b75533?rik=cp%2bBLLPZdFsN3A&pid=ImgRaw&r=0" },
            new Item{ Id = 9, Titulo = "The Witcher 3: Wild Hunt", Genero = "RPG", Ano = 2015, Consola = "PlayStation 4",
                       Descripcion = "Cazador de monstruos en mundo abierto", Desarrollador = "CD Projekt Red", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2020, 9, 10), HorasDedicadas = 120, Calificacion = 10, ImagenUrl = "https://gametimes.com.br/wp-content/uploads/2023/12/The-Witcher-3-requisitos.jpg" },
            new Item { Id = 10, Titulo = "Dark Souls", Genero = "RPG", Ano = 2011, Consola = "PlayStation 3",
                       Descripcion = "Desafiante aventura en mundo oscuro", Desarrollador = "FromSoftware", Estado = EstadoProgreso.Completado,
                       FechaAdquisicion = new DateTime(2019, 11, 5), HorasDedicadas = 80, Calificacion = 9, ImagenUrl = "https://gaming-cdn.com/images/products/18031/orig/dark-souls-3-remastered-pc-steam-cover.jpg?v=1753367298"}
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

        [HttpGet]
        public IActionResult Agregar() => View();

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            // Validación de modelo integrada
            if (!ModelState.IsValid) return View(item);

            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
            return RedirectToAction("Index");
        }
    }
}