using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public interface IItemRepository
    {
        void Add(Item item);
        IEnumerable<Item> GetAll();
        Item Find(string id);
        Item Remove(string id);
        void Update(Item item);
    }
}