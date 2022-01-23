using FO.UI.Data.Looups;
using FO.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        readonly IFriendLookupDataService _FriendLookupDataService;
        readonly IEventAggregator _EventAggregator;
        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            this._EventAggregator = eventAggregator;
            this._FriendLookupDataService = friendLookupDataService;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _EventAggregator.GetEvent<AfterFriendSaved_Event>().Subscribe(AfterFriendSaved);
        }
        public ObservableCollection<NavigationItemViewModel> Friends { get; }
        //private NavigationItemViewModel? _SelectedFriend;

        //public NavigationItemViewModel? SelectedFriend
        //{
        //    get { return _SelectedFriend; }
        //    set
        //    {
        //        _SelectedFriend = value;
        //        OnPropertyChanged();
        //        if (_SelectedFriend != null)
        //        {
        //            _EventAggregator.GetEvent<OpenFriendDetailView_Event>().Publish(_SelectedFriend.Id);
        //        }
        //    }
        //}

        public async Task LoadAsync()
        {
            var lookup = await _FriendLookupDataService.GetFriendLookupAsync();
            if (lookup == null)
                return;
            Friends.Clear();
            foreach (var friend in lookup)
            {
                Friends.Add(new NavigationItemViewModel(friend.Id, friend.DisplayMember, _EventAggregator));
            }
        }
        void AfterFriendSaved(AfterFriendSaved_Event_Args obj)
        {
            var lookupItem = Friends.Single(c => c.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }
    }
}
