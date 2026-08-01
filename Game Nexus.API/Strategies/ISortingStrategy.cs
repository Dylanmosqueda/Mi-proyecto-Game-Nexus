using System.Collections.Generic;
using Game_Nexus.API.Models;

namespace Game_Nexus.API.Strategies
{
    public interface ISortingStrategy
    {
        IEnumerable<Item> Sort(IEnumerable<Item> items);
    }
}

