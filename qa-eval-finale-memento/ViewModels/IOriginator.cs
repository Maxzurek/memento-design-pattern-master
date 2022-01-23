using qa_eval_finale_memento.mementos;

namespace qa_eval_finale_memento.ViewModels
{
    public interface IOriginator
    {
        public IMemento SaveState();
        public void RestoreState(IMemento memento);
    }
}
