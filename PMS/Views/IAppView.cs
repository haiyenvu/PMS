using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using PMS.Views;

namespace System.Windows
{ 
    public interface IAppView : IDisposable, System.Mvc.IView
    {
        object[] GetActions();
        string MainCaption { get; }
    }
}
