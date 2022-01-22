using FO.Model;
using FO.UI.Event;
using FO.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FO.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        #region Constructor

        public FriendDetailViewModel(IFriendDataService dataService, IEventAggregator eventAggregator)
        {
            this._DataService = dataService;
            this._EventAggregator = eventAggregator;
            _EventAggregator.GetEvent<OpenFriendDetailView_Event>().Subscribe(OnOpenFriendDetailView);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        #endregion

        #region fields
        readonly IFriendDataService _DataService;
        readonly IEventAggregator _EventAggregator;
        private Friend_Wrapper? friend;

        #endregion

        #region properties

        public Friend_Wrapper? Friend
        {
            get { return friend; }
            set
            {
                friend = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }
        
        #endregion

        #region Methods

        public async Task LoadAsync(int id)
        {
            if (_DataService == null)
                return;
            var data = await _DataService.GetByIdAsync(id);
            if (data == null)
                return;

            Friend = new Friend_Wrapper(data);
        }
        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }
        bool OnSaveCanExecute()
        {
            //TODO: check fried is valid
            return true;
        }
        private void OnSaveExecute()
        {
            if (Friend == null)
                return;
            _DataService.SaveAsync(Friend.Model);
            _EventAggregator.GetEvent<AfterFriendSaved_Event>().Publish(new AfterFriendSaved_Event_Args
            {
                Id = Friend.Id,
                DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
            });
        }

        #endregion
    }
}
