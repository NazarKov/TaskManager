using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Navigation.Interface
{
    public interface IDialogService
    {
        Task<bool> ConfirmAsync(string title, string message, string accept, string cancel);
    }
}
