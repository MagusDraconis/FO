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

        public FriendDetailViewModel(IFriendRepository repository, IEventAggregator eventAggregator)
        {
            this._FriendRepository = repository;
            this._EventAggregator = eventAggregator;
            
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        #endregion

        #region fields
        readonly IFriendRepository _FriendRepository;
        readonly IEventAggregator _EventAggregator;
        private Friend_Wrapper? _Friend;
        bool _HasChanges;

        #endregion

        #region properties

        public Friend_Wrapper? Friend
        {
            get { return _Friend; }
            set
            {
                _Friend = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }

        public bool HasChanges
        {
            get => _HasChanges;
            set
            {
                if (_HasChanges == value)
                    return;                

                _HasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods

        public async Task LoadAsync(int id)
        {
            if (_FriendRepository == null)
                return;
            var friend = await _FriendRepository.GetByIdAsync(id);
            if (friend == null)
                return;

            Friend = new Friend_Wrapper(friend);
            Friend.PropertyChanged += (sender, e) =>
            {
                if (!HasChanges)
                    HasChanges = _FriendRepository.HasChanges();

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }


        bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }
        private async void OnSaveExecute()
        {
            if (Friend == null)
                return;
            await _FriendRepository.SaveAsync();
            HasChanges = _FriendRepository.HasChanges();
            _EventAggregator.GetEvent<AfterFriendSaved_Event>().Publish(new AfterFriendSaved_Event_Args
            {
                Id = Friend.Id,
                DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
            });
        }

        #endregion
    }
}
