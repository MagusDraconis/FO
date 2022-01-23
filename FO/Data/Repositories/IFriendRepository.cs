using FO.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI
{
    public interface IFriendRepository
    {
        Task<Friend?> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
    }
}
