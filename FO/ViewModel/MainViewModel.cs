using FO.Model;
using FO.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FO.UI
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
        {
            this.FriendDetailViewModel = friendDetailViewModel;
            this.NavigationViewModel = navigationViewModel; 
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IFriendDetailViewModel FriendDetailViewModel { get; }

        public async Task LoadAsync()
        {
            if(NavigationViewModel == null)
                return;
            await NavigationViewModel.LoadAsync();
        }
    }
}
