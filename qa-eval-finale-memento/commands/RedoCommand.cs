using qa_eval_finale_memento.caretakers;

namespace qa_eval_finale_memento.commands
{
    public class RedoCommand : CommandBase
    {
        private readonly Caretaker careTaker;

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
