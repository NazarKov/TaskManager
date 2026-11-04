using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Services.Navigation.Interface;

namespace TaskManager.Services.Navigation
{
    internal class ShellNavigationService : INavigationService
    {
        public Task GoToAsync(string route, IDictionary<string, object>? parameters = null)
        => parameters is null
            ? Shell.Current.GoToAsync(route)
            : Shell.Current.GoToAsync(route, parameters);

        public Task GoBackAsync() => Shell.Current.GoToAsync("..");
    }
}
