using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyUML.Command
    {
    class RelayCommand : ICommand
    {
        //it's a generic Class, Functions are implemented in the Vermodel and are passed as Delegate
        private Action WhatToExecute;
        private Func<bool> WhenToExecute;
        public RelayCommand(Action What, Func<bool> When)
        {
            WhatToExecute = What;
            WhenToExecute = When;
        }
        public bool CanExecute(object parameter)
        {
            return WhenToExecute();
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            WhatToExecute();
        }
    }
}