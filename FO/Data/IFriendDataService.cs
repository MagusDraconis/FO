using FO.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI
{
    public interface IFriendDataService
    {
        Task<Friend?> GetByIdAsync(int id);
        Task SaveAsync(Friend friend);
    }
}
