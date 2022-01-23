using qa_eval_finale_memento.caretakers;
using qa_eval_finale_memento.commands;
using qa_eval_finale_memento.mementos;
using qa_eval_finale_memento.states;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace qa_eval_finale_memento.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IOriginator
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand undoCommand { get; }
        public ICommand redoCommand { get; }
        public ICommand eraseCommand { get; }
        public ICommand pasteCommand { get; }
        private readonly Caretaker caretaker;

        /**********************************************************************************************************
        * Constructor
        ***********************************************************************************************************/
        public MainViewModel()
        {
            caretaker = new Caretaker(this);
            undoCommand = new UndoCommand(caretaker);
            redoCommand = new RedoCommand(caretaker);
            eraseCommand = new EraseCommand(caretaker);
            pasteCommand = new PasteCommand(caretaker);
            UndosHistory = caretaker.GetUndosHistory();
        }

        /**********************************************************************************************************
        * Properties
        ***********************************************************************************************************/
        public bool StateRestored { get; private set; } = false;

        private string text = "";
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private int caretPosition;
        public int CaretPosition
        {
            get
            {
                return caretPosition;
            }
            set
            {
                caretPosition = value;
                OnPropertyChanged(nameof(CaretPosition));
            }
        }

        private string undosHistory = "";
        public string UndosHistory
        {
            get
            {
                return undosHistory;
            }
            set
            {
                undosHistory = value;
                OnPropertyChanged(nameof(UndosHistory));
            }
        }

        private string redosHistory = "";
        public string RedosHistory
        {
            get
            {
                return redosHistory;
            }
            set
            {
                redosHistory = value;
                OnPropertyChanged(nameof(RedosHistory));
            }
        }

        /**********************************************************************************************************
        * Interface implementation
        ***********************************************************************************************************/
        public IMemento SaveState()
        {
            IState state = new TextBoxState(Text.TrimEnd(), CaretPosition);

            return new ConcreteMemento(state);
        }

        public void RestoreState(IMemento memento)
        {
            if (memento is not ConcreteMemento)
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            StateRestored = true;

            IState state = memento.GetState();

            if (state is TextBoxState textBoxState)
            {
                Text = textBoxState.Text;
                CaretPosition = textBoxState.CaretPosition;
            }
        }

        /**********************************************************************************************************
        * Private Methods
        ***********************************************************************************************************/
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Text))
            {
                handleTextChanged();
                UndosHistory = caretaker.GetUndosHistory();
                RedosHistory = caretaker.GetRedosHistory();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void handleTextChanged()
        {
            if (StateRestored)
            {
                StateRestored = !StateRestored;
            }
            else
            {
                List<string> baseInputs = new("a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,1,2,3,4,5,6,7,8,9,0".Split(","));

                if (Text.Length == 0)
                {
                    return;
                }
                else if (Text.Length > 1 && CaretPosition != Text.Length)
                {
                    if (Text[Text.Length - 1] == ' ' && Text[Text.Length - 2] != ' ')
                    {
                        caretaker.Backup();
                    }
                }
            }
        }
    }
}
