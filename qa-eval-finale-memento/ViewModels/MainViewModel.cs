using qa_eval_finale_memento.caretakers;
using qa_eval_finale_memento.commands;
using qa_eval_finale_memento.mementos;
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
            UndosHistory = caretaker.GetUndosHistory();
        }

        /**********************************************************************************************************
        * Properties
        ***********************************************************************************************************/
        public bool StateRestored { get; private set; } = false;

        private string state = "";
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
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
            int stateLength = State.Length;
            string trimmedState = State.TrimEnd();

            return new ConcreteMemento(trimmedState, (CaretPosition < State.Length ? CaretPosition + 1 : CaretPosition));
        }

        public void RestoreState(IMemento memento)
        {
            if(memento is not ConcreteMemento)
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            StateRestored = true;
            State = memento.GetState();
            CaretPosition = memento.GetCaretPosition();
        }

        /**********************************************************************************************************
        * Private Methods
        ***********************************************************************************************************/
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(State))
            {
                handleStateChanged();
                UndosHistory = caretaker.GetUndosHistory();
                RedosHistory = caretaker.GetRedosHistory();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void handleStateChanged()
        {
            if (StateRestored)
            {
                StateRestored = !StateRestored;
            }
            else
            {
                List<string> baseInputs = new("a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,1,2,3,4,5,6,7,8,9,0".Split(","));

                if(State.Length == 0)
                {
                    return;
                }
                if(!baseInputs.Contains(State[State.Length - 1].ToString().ToLower()))
                {
                    caretaker.Backup();
                }
                else if(State.Length > 1)
                {
                    if(State[State.Length - 1] == ' ' && State[State.Length - 2] != ' ')
                    {
                        caretaker.Backup();
                    }
                }
            }
        }
    }
}
