using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        bool HasChanges { get; }

        Task LoadAsync(int id);
    }
}
