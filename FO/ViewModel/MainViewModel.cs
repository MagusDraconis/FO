using FO.UI.Event;
using FO.UI.View.Services;
using FO.UI.ViewModel;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _MessageDialogService = messageDialogService;
            this.NavigationViewModel = navigationViewModel;
            this._FriendDetailViewModelCreator = friendDetailViewModelCreator;
            this._EventAggregator = eventAggregator;
            _EventAggregator.GetEvent<OpenFriendDetailView_Event>().Subscribe(OnOpenFriendDetailView);
        }


        IFriendDetailViewModel _FriendDetailViewModel = null!; //https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
        readonly IEventAggregator _EventAggregator;
        readonly Func<IFriendDetailViewModel> _FriendDetailViewModelCreator;        
        readonly IMessageDialogService _MessageDialogService;

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get => _FriendDetailViewModel;
            private set
            {
                if (_FriendDetailViewModel == value)
                    return;

                _FriendDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            if(NavigationViewModel == null)
                return;
            await NavigationViewModel.LoadAsync();
        }
        private async void OnOpenFriendDetailView(int friendId)
        {
            
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _MessageDialogService.ShowOkChancelEialog("You've made changes. Navigate away will lose changes?", "Question");

                if (result == MessageDialogResult.Cancel)
                    return;
            }

            FriendDetailViewModel = _FriendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);

        }
    }
}
