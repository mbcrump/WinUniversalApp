using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TipAndShare
{
    public class DelegateCommand : ICommand
    {
        Action<object> executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

        public event EventHandler CanExecuteChanged = null;
    }
}
