﻿using System;
using System.Windows.Input;

namespace FileParserWPF.ViewModels.Base
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public virtual bool CanExecute(object parameter) => true;
        public abstract void Execute(object parameter);
    }
}
