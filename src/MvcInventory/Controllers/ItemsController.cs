using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcLibrary.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcLibrary.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository items;
        private readonly ILogger logger;

        public ItemsController(IItemRepository items, ILogger<ItemsController> logger)
        {
            this.items = items;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return this.items.GetAll();
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetById(string id)
        {
            var item = this.items.Find(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Item item)
        {
            if (item == null)
            {
                return this.BadRequest();
            }

            this.items.Add(item);

            this.logger.LogTrace("Added {0} by {1}", item.String, item.CreatedBy);

            return this.CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Item item)
        {
            if (item.Id != id)
            {
                return this.BadRequest();
            }

            var existingItem = this.items.Find(id);
            if (existingItem == null)
            {
                return this.NotFound();
            }

            this.items.Update(item);

            this.logger.LogTrace(
                "Updated {0} by {1} to {2} by {3}",
                existingItem.String,
                existingItem.CreatedBy,
                item.String,
                item.CreatedBy);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public NoContentResult Delete(string id)
        {
            this.items.Remove(id);
            return new NoContentResult();
        }
    }
}
