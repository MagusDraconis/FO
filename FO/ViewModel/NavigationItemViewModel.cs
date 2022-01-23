using FO.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;
using System.Windows.Input;

namespace FO.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {


        public NavigationItemViewModel(int id, string? displayMember, IEventAggregator eventAggregator)
        {
            _EventAggregator = eventAggregator;
            DisplayMember = displayMember;
            Id = id;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
        }
        public int Id { get; }
        string? _DisplayMember;
        readonly IEventAggregator _EventAggregator;
        public string? DisplayMember
        {
            get => _DisplayMember;
            set
            {
                if (_DisplayMember == value)
                {
                    return;
                }

                _DisplayMember = value;
                OnPropertyChanged();
            }
        }
        public ICommand OpenFriendDetailViewCommand { get;  }
        void OnOpenFriendDetailView()
        {
            _EventAggregator.GetEvent<OpenFriendDetailView_Event>().Publish(Id);
        }
    }
}
