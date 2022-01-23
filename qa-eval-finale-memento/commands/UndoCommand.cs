using qa_eval_finale_memento.caretakers;

namespace qa_eval_finale_memento.commands
{
    public class UndoCommand : CommandBase
    {
        private readonly Caretaker careTaker;

        public UndoCommand(Caretaker careTaker)
        {
            this.careTaker = careTaker;
        }

        public override void Execute(object? parameter)
        {
            careTaker.Undo();
        }
    }
}
