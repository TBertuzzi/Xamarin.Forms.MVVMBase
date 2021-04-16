using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms.MVVMBase.Services.Navigation;

namespace Xamarin.Forms
{
    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, NavigationParameters> arguments = new ConditionalWeakTable<Page, NavigationParameters>();

        public static IDictionary<string, object> NavigationArgs(this Page page)
        {
            NavigationParameters parameters = null;
            arguments.TryGetValue(page, out parameters);
            return parameters;
        }

        public static void AddNavigationArgs(this Page page, NavigationParameters parameters)
        {
            arguments.Add(page, parameters);
        }

        public static void RemoveNavigationArgs(this Page page)
        {
            arguments.Remove(page);
        }
    }
}
