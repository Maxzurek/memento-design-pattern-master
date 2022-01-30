using qa_eval_finale_memento.caretakers;
using System.Windows;
using System.Windows.Controls;

namespace qa_eval_finale_memento.commands
{
    internal class PasteCommand : CommandBase
    {
        private readonly Caretaker careTaker;

        public PasteCommand(Caretaker careTaker)
        {
            this.careTaker = careTaker;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is TextBox textBox)
            {
                string text = Clipboard.GetText();

                if (text.Length > 0)
                {
                    int pasteCaretIndex = textBox.CaretIndex;
                    textBox.CaretIndex = textBox.Text.Length - 1;

                    careTaker.Backup();

                    textBox.Text = textBox.Text.Insert(pasteCaretIndex, text);
                    textBox.CaretIndex = pasteCaretIndex + text.Length;

                    careTaker.Backup();
                }
            }
        }
    }
}
