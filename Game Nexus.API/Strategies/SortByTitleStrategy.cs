using System.Collections.Generic;
using System.Linq;
using Game_Nexus.API.Models;

namespace Game_Nexus.API.Strategies
{
    public class SortByTitleStrategy : ISortingStrategy
    {
        public IEnumerable<Item> Sort(IEnumerable<Item> items) =>
            items.OrderBy(i => i.Titulo);
    }
}
