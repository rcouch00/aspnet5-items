using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MvcLibrary.Models
{
    public class ItemRepository : IItemRepository
    {
        private readonly ConcurrentDictionary<string, Item> items;
        private int nextId = 0;

        public ItemRepository()
        {
            this.items = new ConcurrentDictionary<string, Item>();
            this.Add(new Item { String = "RESTful API with ASP.NET Core MVC 1.0", CreatedBy = "Ryan Couch" });
        }

        public void Add(Item item)
        {
            if (item == null)
            {
                return;
            }

            this.nextId++;
            item.Id = nextId.ToString();

            this.items.TryAdd(item.Id, item);
        }

        public Item Find(string id)
        {
            Item item;
            this.items.TryGetValue(id, out item);
            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            return this.items.Values.OrderBy(b => b.Id);
        }

        public Item Remove(string id)
        {
            Item item;
            this.items.TryRemove(id, out item);
            return item;
        }

        public void Update(Item item)
        {
            this.items[item.Id] = item;
        }
    }
}
