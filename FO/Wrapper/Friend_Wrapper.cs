using FO.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FO.UI.Wrapper
{
    public class Friend_Wrapper : Model_Wrapper<Friend>
    {
        public Friend_Wrapper(Friend model) : base(model)
        {
        }

        public int Id => Model.Id;
        public string? FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? LastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Email
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {            
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                       yield return "Robots are not valid Friends";
                    }
                    break;
                case nameof(LastName):
                    if (string.Equals(LastName, "", StringComparison.OrdinalIgnoreCase))
                        yield return "Empty string is not allowed";
                    break;
                 case nameof(Email):
                    if (Email == null || string.IsNullOrWhiteSpace(Email))
                        break;
                    if(!Email.Contains('@'))
                        yield return "It's not a valid email address";
                    break;
                default:
                    break;
            
        }
    }
    }
}
