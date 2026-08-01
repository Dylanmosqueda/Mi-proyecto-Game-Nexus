using System.Collections.Generic;
using Game_Nexus.API.Models;
using Game_Nexus.API.Strategies;

namespace Game_Nexus.API.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetAll(string? genero = null, ISortingStrategy? sortingStrategy = null);
        Item? GetById(int id);
        Item Create(Item item);
    }
}
