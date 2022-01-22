using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        Task LoadAsync(int id);
    }
}
