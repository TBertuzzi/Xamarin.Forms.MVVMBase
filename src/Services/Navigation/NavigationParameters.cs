using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xamarin.Forms.MVVMBase.Services.Navigation
{
    public class NavigationParameters : Dictionary<string, object>
    {
        public NavigationState NavigationState { get; set; }
        public NavigationParameters() { }
        public NavigationParameters(string key, object value)
        {
            Add(key, value);
        }
        public NavigationParameters(string queryString)
        {
            var parameters = ParseQueryString(queryString);
            foreach (var parameter in parameters)
                Add(parameter.Key, parameter.Value);
        }

        public T GetValue<T>(string parameterName)
        {
            if (!ContainsKey(parameterName))
                throw new ArgumentOutOfRangeException(parameterName);

            return (T)Convert.ChangeType(this[parameterName], typeof(T));
        }

        private Dictionary<string, string> ParseQueryString(string queryString)
        {
            return Regex.Matches(queryString, "([^?=&]+)(=([^&]*))?")
                        .Cast<Match>()
                        .ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
        }
    }

    public enum NavigationState
    {
        Init,
        Forward,
        Backward
    }
}
