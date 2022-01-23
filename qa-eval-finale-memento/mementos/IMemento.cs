using qa_eval_finale_memento.states;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qa_eval_finale_memento.mementos
{
    public interface IMemento
    {
        string GetName();

        IState GetState();

        DateTime GetDate();
    }
}
