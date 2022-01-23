using System;
using System.ComponentModel;
using System.Windows.Input;

namespace qa_eval_finale_memento.commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
