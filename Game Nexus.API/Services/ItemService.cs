using System;
using System.Collections.Generic;
using System.Linq;
using Game_Nexus.API.Data;
using Game_Nexus.API.Models;
using Game_Nexus.API.Strategies;

namespace Game_Nexus.API.Services
{
    public class ItemService : IItemService
    {
        public IEnumerable<Item> GetAll(string? genero = null, ISortingStrategy? sortingStrategy = null)
        {
            var items = GameNexusDb.Instance.Items;

            IEnumerable<Item> result = string.IsNullOrEmpty(genero)
                ? items
                : items.Where(i => i.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase));

            if (sortingStrategy != null)
            {
                result = sortingStrategy.Sort(result);
            }

            return result;
        }

        public Item? GetById(int id) => GameNexusDb.Instance.Items.FirstOrDefault(i => i.Id == id);

        public Item Create(Item item)
        {
            item.Id = GameNexusDb.Instance.Items.Any() ? GameNexusDb.Instance.Items.Max(i => i.Id) + 1 : 1;
            GameNexusDb.Instance.Items.Add(item);
            return item;
        }
    }
}
