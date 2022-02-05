//MIT License

using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IWishlistService
    {
        Task Add(WishlistItem item);
        Task DeleteById(int id);
        Task<bool> ExistsWithId(int id);
        Task<bool> ExistsWithTitle(string title);
        Task<IEnumerable<WishlistItem>> GetAll();
        Task<IEnumerable<WishlistItem>> GetByType(ItemType type);
        Task Update(WishlistItem item);
    }
}