using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FO.UI.Wrapper
{
    public class Model_Wrapper<T> : NotifyDataErrorInfoBase
    {
        public Model_Wrapper(T model)
        {
            Model = model;
        }
        public T Model { get; set; }

        protected virtual TValue? GetValue<TValue>([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                return default;

            return (TValue?)typeof(T)?.GetProperty(propertyName)?.GetValue(Model);
        }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null || value == null)
                return;
            var currentValue = GetValue<TValue>(propertyName);

            if (currentValue != null && value.Equals(currentValue))
                return;

            typeof(T)?.GetProperty(propertyName)?.SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName);
        }

        protected virtual IEnumerable<string>? ValidateProperty(string propertyName)
        {
            //Todo: implement Validatioin
            return null;
        }


        private void ValidatePropertyInternal(string propertyName)
        {
            ClearErrors(propertyName);
            var errors = ValidateProperty(propertyName);
            if (errors == null)
                return;
            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }
    }
}
