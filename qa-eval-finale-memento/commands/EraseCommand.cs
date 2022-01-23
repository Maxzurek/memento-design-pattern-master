using qa_eval_finale_memento.caretakers;
using System.Windows.Controls;

namespace qa_eval_finale_memento.commands
{
    public class EraseCommand : CommandBase
    {
        private readonly Caretaker caretaker;

        public EraseCommand(Caretaker caretaker)
        {
            this.caretaker = caretaker;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is TextBox textBox)
            {
                if (textBox.Text.Length == 0)
                {
                    return;
                }
                else if (textBox.SelectedText.Length > 1)
                {
                    int caretIndex = textBox.CaretIndex;
                    string selectedText = textBox.SelectedText;
                    textBox.Text = textBox.Text.Remove(caretIndex, selectedText.Length);
                    textBox.CaretIndex = caretIndex;
                    caretaker.Backup();
                }
                else
                {
                    int caretIndex = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Remove(caretIndex - 1, 1);
                    textBox.CaretIndex = caretIndex == 0 ? 0 : caretIndex - 1;
                    caretaker.Backup();
                }
            }
        }
    }
}
