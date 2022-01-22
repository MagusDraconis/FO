using System;
using System.Linq;

namespace FO.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {


        public NavigationItemViewModel(int id, string? displayMember)
        {
            DisplayMember = displayMember;
            Id = id;
        }
        public int Id { get; }
        string? displayMember;
        public string? DisplayMember
        {
            get => displayMember;
            set
            {
                if (displayMember == value)
                {
                    return;
                }

                displayMember = value;
                OnPropertyChanged();
            }
        }
    }
}
