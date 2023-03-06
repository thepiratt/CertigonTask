using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Models.Items;

namespace CertigonTask_API_V3.Services.ItemService
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Item?> GetSingleItem(int id);
        Task<Item?> UpdateItem(int id, ItemUpdateVM request);
        Task<Item?> DeleteItem(int id);
    }
}
