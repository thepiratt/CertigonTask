using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace CertigonTask_API_V3.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public async Task<Item?> DeleteItem(int id)
        {
            var item = await _dbContext.Item.FindAsync(id);
            if (item == null)
                return null;

            _dbContext.Item.Remove(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<List<Item>> GetAllItems()
        {
            var items = await _dbContext.Item.ToListAsync();
            return items;
        }

        public async Task<Item?> GetSingleItem(int id)
        {
            var item = await _dbContext.Item.FindAsync(id);
            if (item is null)
                return null;

            return item;
        }

        public async Task<Item?> UpdateItem(int id, ItemUpdateVM req)
        {
            Item item;

            if (id == 0)
            {
                item = new Item
                {
                    created_time = DateTime.Now
                };
                _dbContext.Add(item);
            }
            else
            {
                item = await _dbContext.Item.FindAsync(id);
                if (item == null)
                    return null;
            }

            item.Name = req.Name.RemoveTags();
            item.Description = req.Description.RemoveTags();
            item.Price = req.Price;
            item.Category = req.Category;


            await _dbContext.SaveChangesAsync();

            return item;
        }
    }
}
