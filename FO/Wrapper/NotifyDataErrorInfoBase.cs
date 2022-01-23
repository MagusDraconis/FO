using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FO.UI.Wrapper
{
    
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _ErrosByPropertyName = new();

        public bool HasErrors => _ErrosByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        
        public IEnumerable GetErrors(string? propertyName)
        {            
            if (propertyName == null)
                return null!;

            if (_ErrosByPropertyName.ContainsKey(propertyName))
                return _ErrosByPropertyName[propertyName];
            return null!;
        }



        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }
        protected void AddError(string propertyName, string error)
        {
            if (!_ErrosByPropertyName.ContainsKey(propertyName))
            {
                _ErrosByPropertyName[propertyName] = new List<string>();
            }
            if (!_ErrosByPropertyName[propertyName].Contains(error))
            {
                _ErrosByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_ErrosByPropertyName.ContainsKey(propertyName))
            {
                _ErrosByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
