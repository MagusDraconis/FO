using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null!)
        {
            if (propertyName == null || value == null)
                return;

            typeof(T).GetProperty(propertyName)?.SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        protected virtual IEnumerable<string>? ValidateProperty(string propertyName)
        {
            // implement validation on sub class (override)
            return null;
        }


        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);            

            ValidateAnnotationErrors(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }

        private void ValidateAnnotationErrors(string propertyName, object currentValue)
        {
            if (Model == null)
                return;
            var results = new List<ValidationResult>();
            var validateContext = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, validateContext, results);
            foreach (var result in results)
            {
                if (result == null || result.ErrorMessage == null)
                    continue;

                AddError(propertyName, result.ErrorMessage);
            }
        }

        private void ValidateCustomErrors(string propertyName)
        {
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
