using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Services.Navigation.Interface;

namespace TaskManager.Services.Navigation
{
    internal class DialogService : IDialogService
    {
        public Task<bool> ConfirmAsync(string title, string message, string accept, string cancel)
        => Shell.Current.DisplayAlertAsync(title, message, accept, cancel);
    }
}
