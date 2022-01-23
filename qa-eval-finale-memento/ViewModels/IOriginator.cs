using qa_eval_finale_memento.mementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qa_eval_finale_memento.ViewModels
{
    public  interface IOriginator
    {
        public IMemento SaveState();
        public void RestoreState(IMemento memento);
    }
}
