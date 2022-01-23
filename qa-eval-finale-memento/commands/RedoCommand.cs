using qa_eval_finale_memento.caretakers;
using System.Windows.Controls;

namespace qa_eval_finale_memento.commands
{
    public class RedoCommand : CommandBase
    {
        private Caretaker careTaker;

        public RedoCommand(Caretaker careTaker)
        {
            this.careTaker = careTaker;
        }

        public override void Execute(object? parameter)
        {
            careTaker.Redo();
        }
    }
}
