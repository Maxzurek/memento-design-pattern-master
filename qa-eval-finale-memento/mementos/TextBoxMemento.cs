using qa_eval_finale_memento.states;
using System;

namespace qa_eval_finale_memento.mementos
{
    internal class TextBoxMemento : IMemento
    {
        #region Constructor

        public TextBoxMemento(IState state)
        {
            State = state;
            Date = DateTime.Now;
        }

        #endregion

        #region Properties
        private string Name
        {
            get { return $"{Date} : {State}"; }
        }

        private IState State { get;  set; }
        private DateTime Date { get;  set; }

        #endregion

        #region Interface Implementation
        public string GetName()
        {
            return Name;
        }

        public IState GetState()
        {
            return State;
        }

        public DateTime GetDate()
        {
            return Date;
        }

        #endregion
    }
}