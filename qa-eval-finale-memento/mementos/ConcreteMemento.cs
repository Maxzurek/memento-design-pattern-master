using System;

namespace qa_eval_finale_memento.mementos
{
    internal class ConcreteMemento : IMemento
    {
        public string Name
        {
            get { return $"{Date} : \"{State}\" - {CaretPosition}"; }
        }

        public string State { get; private set; } = "";
        public int CaretPosition { get; private set; } = 0;
        public DateTime Date { get; private set; }

        public ConcreteMemento(string state, int caretPosition)
        {
            State = state;
            Date = DateTime.Now;
            CaretPosition = caretPosition;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetState()
        {
            return State;
        }

        public DateTime GetDate()
        {
            return Date;
        }

        public int GetCaretPosition()
        {
            return CaretPosition;
        }
    }
}