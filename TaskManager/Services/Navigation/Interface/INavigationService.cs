using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Navigation.Interface
{
    public interface INavigationService
    {
        Task GoToAsync(string route, IDictionary<string, object>? parameters = null);
        Task GoBackAsync();
    }
}
