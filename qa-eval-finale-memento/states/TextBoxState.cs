namespace qa_eval_finale_memento.states
{
    public class TextBoxState : IState
    {
        #region Constuctor

        public TextBoxState(string text, int caretPosition)
        {
            Text = text;
            CaretPosition = caretPosition;
        }

        #endregion

        #region Properties

        public string Text { get; set; } = "";
        public int CaretPosition { get; set; } = 0;

        #endregion

        #region Overriden Method

        public override string ToString()
        {
            return $"Text: \"{Text}\" ; Caret: {CaretPosition}";
        }

        #endregion
    }
}
