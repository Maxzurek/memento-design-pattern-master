using qa_eval_finale_memento.caretakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace qa_eval_finale_memento.commands
{
    internal class PasteCommand : CommandBase
    {
        private Caretaker careTaker;

        public PasteCommand(Caretaker careTaker)
        {
            this.careTaker = careTaker;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is TextBox textBox)
            { 
                string text = Clipboard.GetText();

                if(text.Length > 0)
                {
                    careTaker.Backup();

                    int caretIndex = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(caretIndex, text);
                    textBox.CaretIndex = caretIndex + text.Length;

                    careTaker.Backup();
                }
            }
        }
    }
}
