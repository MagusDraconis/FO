using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FO.UI.Wrapper
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errosByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errosByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return null;

            if (_errosByPropertyName.ContainsKey(propertyName))
                return _errosByPropertyName[propertyName];
            return null;
        }



        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        protected void AddError(string propertyName, string error)
        {
            if (!_errosByPropertyName.ContainsKey(propertyName))
            {
                _errosByPropertyName[propertyName] = new List<string>();
            }
            if (!_errosByPropertyName[propertyName].Contains(error))
            {
                _errosByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errosByPropertyName.ContainsKey(propertyName))
            {
                _errosByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
