using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rentering.Common.Shared.SafeEnums
{
    public abstract class SafeEnumeration : IComparable
    {
        protected readonly int _value;
        protected readonly string _displayName;

        protected SafeEnumeration()
        {
        }

        protected SafeEnumeration(int value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public int Value => _value;

        public string DisplayName => _displayName;

        public override string ToString()
        {
            return DisplayName;
        }

        public string ToValueString()
        {
            return Value.ToString();
        }

        public static IEnumerable<T> GetAll<T>() where T : SafeEnumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as SafeEnumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static int AbsoluteDifference(SafeEnumeration firstValue, SafeEnumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : SafeEnumeration, new()
        {
            var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : SafeEnumeration, new()
        {
            var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        protected static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : SafeEnumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((SafeEnumeration)other).Value);
        }
    }
}
