using qa_eval_finale_memento.states;
using System;

namespace qa_eval_finale_memento.mementos
{
    public interface IMemento
    {
        string GetName();

        IState GetState();

        DateTime GetDate();
    }
}
