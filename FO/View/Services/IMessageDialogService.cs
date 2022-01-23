using System;
using System.Linq;

namespace FO.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkChancelEialog(string text, string title);
    }
}
