using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qa_eval_finale_memento.states
{
    public class TextBoxState : IState
    {
        public TextBoxState(string text, int caretPosition)
        {
            Text = text;
            CaretPosition = caretPosition;
        }

        public string Text { get; set; } = "";
        public int CaretPosition { get; set; } = 0;

        public override string ToString()
        {
            return $"\"{Text}\" - {CaretPosition}";
        }
    }
}
