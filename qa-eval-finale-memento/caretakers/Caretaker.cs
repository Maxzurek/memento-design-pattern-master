using qa_eval_finale_memento.mementos;
using qa_eval_finale_memento.ViewModels;
using System;
using System.Collections.Generic;

namespace qa_eval_finale_memento.caretakers
{
    public class Caretaker
    {
        private readonly IOriginator originator;
        private readonly Stack<IMemento> undos = new();
        private readonly Stack<IMemento> redos = new();

        public Caretaker(IOriginator originator)
        {
            this.originator = originator;
            Backup();
        }

        public void Backup()
        {
            if(undos.Count == 0)
            {
                IMemento? memento;

                if (redos.TryPeek(out memento))
                {
                    undos.Push(memento);
                }
            }
            redos.Clear();
            undos.Push(originator.SaveState());
        }

        public bool Undo()
        {
            if (undos.Count == 0)
            {
                return false;
            }

            IMemento memento = undos.Pop();

            try
            {
                redos.Push(memento);
                originator.RestoreState(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

            return true;
        }

        public bool Redo()
        {
            if (redos.Count == 0)
            {
                return false;
            }

            IMemento memento = redos.Pop();
            undos.Push(memento);

            try
            {
                originator.RestoreState(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

            return true;
        }

        public string GetUndosHistory()
        {
            string history = "-> ";

            foreach (var memento in undos)
            {
                history += memento.GetName();
                history += '\n';
            }

            return history;
        }

        public string GetRedosHistory()
        {
            string history = "-> ";

            foreach (var memento in redos)
            {
                history += memento.GetName();
                history += '\n';
            }

            return history;
        }
    }
}
